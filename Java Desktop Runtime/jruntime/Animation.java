/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.io.IOException;
import javax.microedition.lcdui.*;
import java.util.*;
/**
 *
 * @author Riju
 */
public class Animation
{
        private Vector images = null;
	private int update_delay = 0;
	private int update_counter  = 0;
	private int current_frame = 0;
	private boolean canRepeat = false;
        private GameObject_Scene baseGameObject = null;
        private boolean isRunning = false;
        
        public Animation( GameObject_Scene baseObject , int update_delay , boolean repeat)
	{
                this.baseGameObject = baseObject;
		this.update_delay = update_delay;
		update_counter = 0;
		canRepeat = repeat;
		images = new Vector( );
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
        
	public void addFrame(Image img)
	{
		if (img != null)
		{
			images.addElement( img ); 
		}
	}

	public void addFrame(String name)
	{
		if (name != null)
		{
			if (ResourceManager.getResource(name) != "")
			{
                            try
                            {
				images.addElement(Image.createImage( ResourceManager.getResourceAsStream( name ) ));
                            }
                            catch(IOException ax)
                            {
                            
                            }
			}
		}
	}

	public void setFrame(int frame_id,Image img)
	{
		if (frame_id > -1 && frame_id < images.size())
		{
			images.setElementAt(img,frame_id);
		} 
	}

	public void deleteFrame(int frame_id)
	{
		if (frame_id > -1 && frame_id < images.size( ))
		{
			images.removeElementAt(frame_id );
		} 
	}

	public Image getFrame()
	{
		return (Image) images.elementAt(current_frame); 
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
                                        baseGameObject.setImage((Image) images.elementAt(current_frame));
                                        update_counter = 0;
				 }
                                 else
                                 {
                                     isRunning = false;
                                 }
			}
			else
			{
                                baseGameObject.setImage((Image) images.elementAt(current_frame));
				current_frame++;
                                update_counter = 0;
			}
		}
		else
		{
			update_counter++;
		}	 
	}
}
