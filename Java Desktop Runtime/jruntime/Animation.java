package jruntime;

import jruntime.Resources;
import java.util.*;
import java.awt.image.BufferedImage;
import javax.imageio.*;
import java.io.*;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class Animation
{
	private ArrayList<BufferedImage> images = null;
	private float update_delay = 0.0f;
	private float update_counter  = 0.0f;
	private int current_frame = 0;
	private boolean canRepeat = false;
        private GameObject_Scene baseGameObject = null;
        private boolean isRunning = false;
        
	public Animation( GameObject_Scene baseObject , float update_delay , boolean repeat)
	{
                this.baseGameObject = baseObject;
		this.update_delay = update_delay;
		update_counter = 0.0f;
		canRepeat = repeat;
		images = new ArrayList<BufferedImage>( );
	}

        public void start( )
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;
            }
        }
       
        public boolean isPlaying( )
        {
            return isRunning;
        }
        
	public void addFrame(BufferedImage img)
	{
		if (img != null)
		{
			images.add( img ); 
		}
	}

	public void addFrame(String name)
	{
		if (name != null)
		{
			if (Resources.getResource(name) != "")
			{
                            try
                            {
				images.add(ImageIO.read( Resources.getResourceAsStream( name ) ));
                            }
                            catch(IOException ax)
                            {
                            
                            }
			}
		}
	}

	public void setFrame(int frame_id, BufferedImage img)
	{
		if (frame_id > -1 && frame_id < images.size())
		{
			images.set(frame_id, img);
		} 
	}

	public void deleteFrame(int frame_id)
	{
		if (frame_id > -1 && frame_id < images.size( ))
		{
			images.remove(frame_id );
		} 
	}

	public BufferedImage getFrame()
	{
		return images.get(current_frame); 
	}
        
        public void stop( )
        {
            isRunning = false;
        }
        
	public void update( )
	{
		if (update_counter >= update_delay)
		{
			if (current_frame == images.size( ) - 1)
			{
				 if (canRepeat)
				 {
					current_frame = 0; 
                                        baseGameObject.setImage(images.get(current_frame));
                                        update_counter = 0f;
				 }
                                 else
                                 {
                                     isRunning = false;
                                 }
			}
			else
			{
                                baseGameObject.setImage(images.get(current_frame));
				current_frame++;
                                update_counter = 0f;
			}
		}
		else
		{
			update_counter += 1f;
		}	 
	}
}
