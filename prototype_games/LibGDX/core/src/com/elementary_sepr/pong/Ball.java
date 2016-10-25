package com.elementary_sepr.pong;

import java.util.Random;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.math.MathUtils;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.math.Vector2;

public class Ball {
	static final float radius = 10;
	static final float initSpeed = 6;
	// (x,y) denotes the centre of the ball
	private float x;
	private float y;
	private float vx;
	private float vy;
	private Texture ballTexture;
	private Sprite ballSprite;
	
	public Ball() {
		x = Gdx.graphics.getWidth()/2f;
		y = Gdx.graphics.getHeight()/2f;
		vy = 0f;
		if (MathUtils.randomBoolean())
			vx = initSpeed;
		else
			vx = -initSpeed;
		
		ballTexture = new Texture(Gdx.files.internal("square.png"));
		ballSprite = new Sprite(ballTexture);
		ballSprite.setSize(radius*2, radius*2);
	}
//	
	public void update()
	{
		x += vx;
		y += vy;
	}
	
	public void draw(SpriteBatch batch) 
	{
		// remember that (x,y) is the centre of the ball, but we want to give
		// the bottom-left corner of the texture for drawing
		ballSprite.setPosition(x-radius, y-radius);
		ballSprite.draw(batch);
	}
	
	public Rectangle getHitbox()
	{
		return new Rectangle(x-radius, y-radius, radius*2, radius*2);
	}
	
	public Vector2 getVelocity()
	{
		return new Vector2(vx, vy);
	}
	
	public void bounceOffWall()
	{
		vy *= -1;
	}
	
	public void bounceOffPaddle(Paddle paddle)
	{
		vx *= -1;
		// change the y direction
	    float y_delta = y - (paddle.getHitbox().y + Paddle.height/2f);
	    vy = (2 * y_delta * initSpeed) / Paddle.height;
	}
}
