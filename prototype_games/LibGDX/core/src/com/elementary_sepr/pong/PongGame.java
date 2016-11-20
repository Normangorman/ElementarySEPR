package com.elementary_sepr.pong;

import com.badlogic.gdx.Application;
import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Rectangle;

public class PongGame extends ApplicationAdapter {
	SpriteBatch batch;
	Paddle player1Paddle;
	Paddle player2Paddle;
	Ball ball;
	
	@Override
	public void create () {
		Gdx.app.setLogLevel(Application.LOG_DEBUG);
		batch = new SpriteBatch();
		nextRound();
	}

	@Override
	public void render () {
		// LibGDX has no actual update callback - so we put it at the start
		// of the render method
		update();
		
		// Set background colour
		Gdx.gl.glClearColor(28/255f, 107/255f, 160/255f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		
		// Draw each game object
		batch.begin();
		player1Paddle.draw(batch);
		player2Paddle.draw(batch);
		ball.draw(batch);
		batch.end();
	}
	
	public void update() {
		player1Paddle.update();
		player2Paddle.update();
		ball.update();
		
		checkCollisions();
	}
	
	@Override
	public void dispose () {
		batch.dispose();
	}
	
	private void nextRound()
	{
		ball = new Ball();
		player1Paddle = new Paddle(1);
		player2Paddle = new Paddle(2);
	}
	
	public void checkCollisions() {
		Rectangle ballHitbox = ball.getHitbox();
		Rectangle player1Hitbox = player1Paddle.getHitbox();
		Rectangle player2Hitbox = player2Paddle.getHitbox();
		
		// Collide ball vs upper and lower walls
		if (ball.getVelocity().y < 0 && ballHitbox.y < 0 
			|| ball.getVelocity().y > 0 && ballHitbox.y + ballHitbox.height > Gdx.graphics.getHeight())
		{
			ball.bounceOffWall();
			Gdx.app.log("PongGame", "Ball bounced off wall");
		}
		
		// Collide ball vs side walls (maybe resetting game)
		if (ballHitbox.x < 0 || ballHitbox.x + ballHitbox.width > Gdx.graphics.getWidth())
		{
			nextRound();
			Gdx.app.log("PongGame", "Ball reset");
		}
		
		// Collide ball vs paddles
		if (ballHitbox.overlaps(player1Hitbox) && ball.getVelocity().x < 0) 
		{
			Gdx.app.log("PongGame", "Ball hit p1 paddle");
			ball.bounceOffPaddle(player1Paddle);
		}
		else if (ballHitbox.overlaps(player2Hitbox) && ball.getVelocity().x > 0)
		{
			Gdx.app.log("PongGame", "Ball hit p2 paddle");
			ball.bounceOffPaddle(player2Paddle);
		}
	}
}
