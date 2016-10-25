package com.elementary_sepr.pong;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Rectangle;

public class Paddle {
	static final float width = 22;
	static final float height = 66;
	static final float speed = 10;
	private int playerNum;
	private float x;
	private float y;
	private Texture paddleTexture;
	private Sprite paddleSprite;
	
	public Paddle(int playerNum)
	{
		// playerNum should be 0 for left player and 1 for right player
		assert(playerNum == 1 || playerNum == 2);
		this.playerNum = playerNum;
		switch(playerNum)
		{
		case 1:
			x = 22;
			break;
		case 2:
			x = Gdx.graphics.getWidth() - 44;
			break;
		}
		y = 200;
		
		paddleTexture = new Texture(Gdx.files.internal("square.png"));
		paddleSprite = new Sprite(paddleTexture);
		paddleSprite.setSize(width, height);
	}
	
	void update() {
		switch(playerNum)
		{
		case 1:
			if(Gdx.input.isKeyPressed(Input.Keys.W))
			{
				y += speed;
			}
			else if (Gdx.input.isKeyPressed(Input.Keys.S))
			{
				y -= speed;
			}
			break;
		case 2:
			if(Gdx.input.isKeyPressed(Input.Keys.UP))
			{
				y += speed;
			}
			else if (Gdx.input.isKeyPressed(Input.Keys.DOWN))
			{
				y -= speed;
			}
			break;
		}
	}
	
	void draw(SpriteBatch batch)
	{
		paddleSprite.setPosition(x, y);
		paddleSprite.draw(batch);
	}
	
	Rectangle getHitbox()
	{
		return new Rectangle(x, y, width, height);
	}
}
