var requestAnimFrame = (function () {
    return window.requestAnimationFrame ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame ||
        window.oRequestAnimationFrame ||
        window.msRequestAnimationFrame ||
        function (callback) {
            window.setTimeout(callback, 1000 / 60);
        };
})();

// Create the canvas
var canvas = document.createElement("canvas");
var ctx = canvas.getContext("2d");
canvas.width = 512;
canvas.height = 480;
document.body.appendChild(canvas);
// The main game loop
var lastTime;
function main() {
    var now = Date.now();
    var dt = (now - lastTime) / 1000.0;

    update(dt);
    render();

    lastTime = now;
    requestAnimFrame(main);
};
function init() {
    terrainPattern = ctx.createPattern(resources.get('img/terrain.png'), 'repeat');

    document.getElementById('play-again').addEventListener('click', function () {
        reset();
    });

    reset();
    lastTime = Date.now();
    main();

}

resources.load([
    'img/sprites.png',
    'img/terrain.png'
]);
resources.onReady(init);

// Game state
var player = {
    pos: [0, 0],
    sprite: new Sprite('img/sprites.png', [0, 6], [39, 24], 16, [0, 1])
};

var bullets = [];
var enemies = [];
var explosions = [];

var megalithes = [];
var manna = [];

var lastFire = Date.now();
var gameTime = 0;
var isGameOver;
var terrainPattern;

var score = 0;
var mannaScore = 0;
var scoreEl = document.getElementById('score');
var mannaScoreEl = document.getElementById('manna');

// Speed in pixels per second
var playerSpeed = 200;
var bulletSpeed = 500;
var enemySpeed = 100;

// Update game objects
function update(dt) {
    gameTime += dt;

    handleInput(dt);
    updateEntities(dt);

    // It gets harder over time by adding enemies using this
    // equation: 1-.993^gameTime
    if (Math.random() < 1 - Math.pow(.993, gameTime)) {
        enemies.push({
            pos: [canvas.width,
            Math.random() * (canvas.height - 39)],
            sprite: new Sprite('img/sprites.png', [0, 78], [80, 39],
                6, [0, 1, 2, 3, 2, 1])
        });
    }
    checkCollisions(dt);

    scoreEl.innerHTML = "Score: " + score;
    mannaScoreEl.innerHTML = "Manna score: " + mannaScore;
};

function handleInput(dt) {
    if (input.isDown('DOWN') || input.isDown('s')) {
        player.pos[1] += playerSpeed * dt;
    }

    if (input.isDown('UP') || input.isDown('w')) {
        player.pos[1] -= playerSpeed * dt;
    }

    if (input.isDown('LEFT') || input.isDown('a')) {
        player.pos[0] -= playerSpeed * dt;
    }

    if (input.isDown('RIGHT') || input.isDown('d')) {
        player.pos[0] += playerSpeed * dt;
    }

    if (input.isDown('SPACE') &&
        !isGameOver &&
        Date.now() - lastFire > 100) {
        var x = player.pos[0] + player.sprite.size[0] / 2;
        var y = player.pos[1] + player.sprite.size[1] / 2;

        bullets.push({
            pos: [x, y],
            dir: 'forward',
            sprite: new Sprite('img/sprites.png', [0, 39], [18, 8])
        });
        bullets.push({
            pos: [x, y],
            dir: 'up',
            sprite: new Sprite('img/sprites.png', [0, 50], [9, 5])
        });
        bullets.push({
            pos: [x, y],
            dir: 'down',
            sprite: new Sprite('img/sprites.png', [0, 60], [9, 5])
        });

        lastFire = Date.now();
    }
}

function updateEntities(dt) {
    // Update the player sprite animation
    player.sprite.update(dt);

    for (var i = 0; i < megalithes.length; i++) {
        megalithes[i].sprite.update(dt);
    }

    for (var i = 0; i < manna.length; i++) {
        manna[i].sprite.update(dt);
    }

    // Update all the bullets
    for (var i = 0; i < bullets.length; i++) {
        var bullet = bullets[i];

        switch (bullet.dir) {
            case 'up': bullet.pos[1] -= bulletSpeed * dt; break;
            case 'down': bullet.pos[1] += bulletSpeed * dt; break;
            default:
                bullet.pos[0] += bulletSpeed * dt;
        }

        // Remove the bullet if it goes offscreen
        if (bullet.pos[1] < 0 || bullet.pos[1] > canvas.height ||
            bullet.pos[0] > canvas.width) {
            bullets.splice(i, 1);
            i--;
        }
    }

    // Update all the enemies
    for (var i = 0; i < enemies.length; i++) {
        enemies[i].pos[0] -= enemySpeed * dt;
        enemies[i].sprite.update(dt);

        // Remove if offscreen
        if (enemies[i].pos[0] + enemies[i].sprite.size[0] < 0) {
            enemies.splice(i, 1);
            i--;
        }
    }

    // Update all the explosions
    for (var i = 0; i < explosions.length; i++) {
        explosions[i].sprite.update(dt);

        // Remove if animation is done
        if (explosions[i].sprite.done) {
            explosions.splice(i, 1);
            i--;
        }
    }
}
// Collisions
function collides(x, y, r, b, x2, y2, r2, b2) {
    return !(r <= x2 || x > r2 ||
        b <= y2 || y > b2);
}

function boxCollides(pos, size, pos2, size2) {
    return collides(pos[0], pos[1],
        pos[0] + size[0], pos[1] + size[1],
        pos2[0], pos2[1],
        pos2[0] + size2[0], pos2[1] + size2[1]);
}

function checkCollisions(dt) {
    checkPlayerBounds();

    // Run collision detection for all enemies
    for (var i = 0; i < enemies.length; i++) {
        var pos = enemies[i].pos;
        var size = enemies[i].sprite.size;


        for (var k = 0; k < megalithes.length; k++) {
            var pos3 = megalithes[k].pos;
            var size3 = megalithes[k].sprite.size;

            if (boxCollides(pos, size, pos3, size3)) {
                if (pos[1] >= pos3[1]) {
                    enemies[i].pos[1] += enemySpeed * dt;
                }
                if (pos[1] < pos3[1]) {
                    enemies[i].pos[1] -= enemySpeed * dt;
                }
                if (pos[0] >= pos3[0]) {
                    enemies[i].pos[0] += enemySpeed * dt;
                }
                enemies[i].sprite.update(dt);
            }
        }

        for (var j = 0; j < bullets.length; j++) {
            var pos2 = bullets[j].pos;
            var size2 = bullets[j].sprite.size;

            if (boxCollides(pos, size, pos2, size2)) {
                // Remove the enemy
                enemies.splice(i, 1);
                i--;

                // Add score
                score += 100;

                // Add an explosion
                explosions.push({
                    pos: pos,
                    sprite: new Sprite('img/sprites.png',
                        [0, 117],
                        [39, 39],
                        16,
                        [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                        null,
                        true)
                });

                // Remove the bullet and stop this iteration
                bullets.splice(j, 1);
                break;
            }
        }

        if (boxCollides(pos, size, player.pos, player.sprite.size)) {
            gameOver();
        }
    }

    // Run collision detection for all megalithes
    for (var i = 0; i < megalithes.length; i++) {
        var pos = megalithes[i].pos;
        var size = megalithes[i].sprite.size;

        for (var j = 0; j < bullets.length; j++) {
            var pos2 = bullets[j].pos;
            var size2 = bullets[j].sprite.size;

            if (boxCollides(pos, size, pos2, size2)) {
                // Add an explosion
                explosions.push({
                    pos: pos2,
                    sprite: new Sprite('img/sprites.png',
                        [0, 117],
                        [39, 39],
                        16,
                        [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                        null,
                        true)
                });
                renderEntity(megalithes[i]);

                bullets.splice(j, 1);
                break;
            }
        }


        if (boxCollides(pos, size, player.pos, player.sprite.size)) {
            if (input.isDown('UP') || input.isDown('DOWN')) {
                if (player.pos[1] < pos[1]) {
                    player.pos[1] = pos[1] - player.sprite.size[1] - 1;
                }
                else if (player.pos[1] > pos[1]) {
                    player.pos[1] = pos[1] + size[1] + 1;
                }
            }
            else if (input.isDown('LEFT') || input.isDown('RIGHT')) {
                if (player.pos[0] < pos[0]) {
                    player.pos[0] = pos[0] - player.sprite.size[0] - 1;
                }
                else if (player.pos[0] > pos[0]) {
                    player.pos[0] = pos[0] + size[0] + 1;
                }
            }
        }
    }

    // Run collision detection for all mannas
    for (var i = 0; i < manna.length; i++) {
        var pos = manna[i].pos;
        var size = manna[i].sprite.size;

        if (boxCollides(pos, size, player.pos, player.sprite.size)) {
            // Add an explosion
            explosions.push({
                pos: pos,
                sprite: new Sprite('img/sprites.png', [15, 163], [45, 45],
                    13, [0, 1, 2, 3], null,
                    true)
            });
            manna.splice(i, 1);
            mannaScore += 1;
            generateManna();
        }

        for (var j = 0; j < megalithes.length; j++) {
            var pos1 = megalithes[j].pos;
            var size1 = megalithes[j].sprite.size;
            if (boxCollides(pos, size, pos1, size1)) {
                pos[0] += pos1[0];
            }
        }
    }
}

function checkPlayerBounds() {
    // Check bounds
    if (player.pos[0] < 0) {
        player.pos[0] = 0;
    }
    else if (player.pos[0] > canvas.width - player.sprite.size[0]) {
        player.pos[0] = canvas.width - player.sprite.size[0];
    }

    if (player.pos[1] < 0) {
        player.pos[1] = 0;
    }
    else if (player.pos[1] > canvas.height - player.sprite.size[1]) {
        player.pos[1] = canvas.height - player.sprite.size[1];
    }
}

// Draw everything
function render() {
    ctx.fillStyle = terrainPattern;
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    // Render the player if the game isn't over
    if (!isGameOver) {
        renderEntity(player);
    }

    renderEntities(bullets);
    renderEntities(enemies);
    renderEntities(megalithes);
    renderEntities(explosions);
    renderEntities(manna);
};

function renderEntities(list) {
    for (var i = 0; i < list.length; i++) {
        renderEntity(list[i]);
    }
}

function renderEntity(entity) {
    ctx.save();
    ctx.translate(entity.pos[0], entity.pos[1]);
    entity.sprite.render(ctx);
    ctx.restore();
}

// Game over
function gameOver() {
    document.getElementById('game-over').style.display = 'block';
    document.getElementById('game-over-overlay').style.display = 'block';
    isGameOver = true;
}

// Reset game to original state
function reset() {
    document.getElementById('game-over').style.display = 'none';
    document.getElementById('game-over-overlay').style.display = 'none';
    isGameOver = false;
    gameTime = 0;
    score = 0;
    mannaScore = 0;

    enemies = [];
    bullets = [];

    megalithes = [];
    manna = [];

    player.pos = [50, canvas.height / 2];

    generateMegalithes();
    generateManna();
};

function randomInteger(min, max) {
    let rand = min - 1 + Math.random() * (max - min);
    return Math.round(rand);
}

function generateMegalithes() {

    var megalithesCount = randomInteger(3, 5);

    for (let i = 0; i < megalithesCount; i++) {
        if (randomInteger(1, 2) == 1) {
            var pos = [3, 213];
            var size = [55, 53];
        }
        else {
            var pos = [5, 274];
            var size = [48, 42];
        }
        megalithes.push(
            {
                pos: [Math.random() * (canvas.width - 48),
                Math.random() * (canvas.height - 42)],
                sprite: new Sprite('img/sprites.png', pos, size, 0)
            }
        );
    }

    for (let i = 0; i < megalithes.length; i++) {
        if (boxCollides(player.pos, player.sprite.size, megalithes[i].pos, megalithes[i].sprite.size)) {
            megalithes.splice(i, 1);
            megalithes.push(
                {
                    pos: [Math.random() * (canvas.width - 48),
                    Math.random() * (canvas.height - 42)],
                    sprite: new Sprite('img/sprites.png', pos, size, 0)
                }
            );
        }
    }
}

function generateManna() {
    var chance = randomInteger(0, 10);
    var mannaCount;
    if (manna.length == 0) {
        mannaCount = randomInteger(3, 8);
    }
    else if (manna.length < 3) {
        mannaCount = randomInteger(3 - manna.length, 8 - manna.length);
    }
    else if ((manna.length > 3) && (manna.length < 8)) {
        if (chance > 4) {
            mannaCount = randomInteger(0, 8 - manna.length);
        }
    }

    for (let i = 0; i < mannaCount; i++) {
        manna.push(
            {
                pos: [Math.random() * (canvas.width - 48),
                Math.random() * (canvas.height - 42)],
                sprite: new Sprite('img/sprites.png', [15, 163], [45, 45],
                    2, [0, 1])
            }
        );

    }

    for (let i = 0; i < manna.length; i++) {
        if (boxCollides(player.pos, player.sprite.size, manna[i].pos, manna[i].sprite.size)) {
            manna.splice(i, 1);
            manna.push(
                {
                    pos: [Math.random() * (canvas.width - 48),
                    Math.random() * (canvas.height - 42)],
                    sprite: new Sprite('img/sprites.png', [15, 163], [45, 45],
                        2, [0, 1])
                }
            );
        }
    }
}