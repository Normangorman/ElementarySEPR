-- Some constant definitions
PADDLE_SPEED = 5 -- the paddle is moved this many pixels per frame
PADDLE_WIDTH = 22 -- the paddle is this many pixels wide
PADDLE_HEIGHT = 66
BALL_RADIUS = 10
BALL_INIT_SPEED = 6

-- There is only one data structure in Lua which is a dictionary
-- This syntax below is similar to python dictionaries, e.g.
--      paddle_left = {x:22, y:200}
paddle_left = {
    x = 22,
    y = 200
}

paddle_right = {
    x = love.graphics.getWidth() - 44,
    y = 200
}

ball = {
    x = love.graphics.getWidth() / 2,
    y = love.graphics.getHeight() / 2,
    vx = 0,
    vy = 0,
    radius = 10
}

-- The functions defined below are all 'callback' functions used by the Love2D engine
-- The engine will call the functions at the right times to make the game work
function love.load()
    -- love.load is run once when the game is loaded. It is good for loading settings
    love.graphics.setBackgroundColor(28, 107, 160) -- a nice blue colour

    reset_ball()
end

function love.draw()
    -- love.draw is called once per game frame and should draw all the game objects to the screen
    -- left paddle
    love.graphics.rectangle('fill', paddle_left.x, paddle_left.y, PADDLE_WIDTH, PADDLE_HEIGHT) 
    -- right paddle
    love.graphics.rectangle('fill', paddle_right.x, paddle_right.y, PADDLE_WIDTH, PADDLE_HEIGHT)
    -- ball
    love.graphics.circle('fill', ball.x, ball.y, BALL_RADIUS)
end

function love.update()
    -- love.update is called once per game frame and should update the state of all game objects
    handle_movement()
    handle_collisions()
end

function reset_ball()
    -- resets the ball to it's initial position and gives it a random velocity
    ball.x = love.graphics.getWidth()/2
    ball.y = love.graphics.getHeight()/2

    local starting_direction = love.math.random(1,2)
    if starting_direction == 1 then -- ball moves left to start with
        ball.vx = -BALL_INIT_SPEED
    else -- ball moves right to start with
        ball.vx = BALL_INIT_SPEED
    end
    ball.vy = 0
end

function handle_movement()
    -- checks whether keys are pressed and moves the paddles appropriately
    if love.keyboard.isDown('w') then
        paddle_left.y = paddle_left.y - PADDLE_SPEED 
    elseif love.keyboard.isDown('s') then
        paddle_left.y = paddle_left.y + PADDLE_SPEED
    end

    if love.keyboard.isDown('up') then
        paddle_right.y = paddle_right.y - PADDLE_SPEED 
    elseif love.keyboard.isDown('down') then
        paddle_right.y = paddle_right.y + PADDLE_SPEED
    end

    ball.x = ball.x + ball.vx
    ball.y = ball.y + ball.vy
end

function handle_collisions()
    -- checks for collisions between the ball, paddles and edges of screen
    -- first check ball against top and bottom of screen:
    if ball.y - ball.radius < 0 then
        ball.vy = math.abs(ball.vy)
    elseif ball.y + ball.radius > love.graphics.getHeight() then
        ball.vy = -math.abs(ball.vy)
    end

    -- then check ball against left and right of screen:
    if ball.x - ball.radius < 0 then
        reset_ball()
    elseif ball.x + ball.radius > love.graphics.getWidth() then
        reset_ball()
    end

    -- now check ball against paddles
    -- left paddle
    if ball.y >= paddle_left.y and
       ball.y <= paddle_left.y + PADDLE_HEIGHT and
       ball.x >= paddle_left.x and
       ball.x <= paddle_left.x + PADDLE_WIDTH and
       ball.vx < 0 then
       bounce_ball_off_paddle(paddle_left)
    end

    if ball.y >= paddle_right.y and
       ball.y <= paddle_right.y + PADDLE_HEIGHT and
       ball.x >= paddle_right.x and
       ball.x <= paddle_right.x + PADDLE_WIDTH and
       ball.vx > 0 then
       bounce_ball_off_paddle(paddle_right)
    end
end

function bounce_ball_off_paddle(paddle)
    -- reverse the x direction
    ball.vx = -ball.vx
    -- change the y direction
    local y_delta = ball.y - (paddle.y + PADDLE_HEIGHT/2)
    ball.vy = (2 * y_delta * BALL_INIT_SPEED) / PADDLE_HEIGHT
end
