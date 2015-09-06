/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package jruntime;

import java.io.IOException;
import java.io.InputStream;
import javax.microedition.media.Manager;
import javax.microedition.media.MediaException;

/**
 *
 * @author Riju
 */
public class Audio 
{
    InputStream inp_stm;
    javax.microedition.media.Player aud_player;
    
    public Audio(String file)
    {
        try
        {
            inp_stm = ResourceManager.getResourceAsStream(file);
        
            aud_player =  Manager.createPlayer(inp_stm, "audio/X-wav");
        }
        catch (IOException eax)
        {
            
        }
        catch (MediaException ebx)
        {
            
        }
    }
    
    public void start( )
    {
        try
        {   
            aud_player.start();
        }
        catch (MediaException eax)
        {
            
        }
    }
    
    public boolean isPlaying( )
    {
        if (aud_player.getState() == javax.microedition.media.Player.CLOSED)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void stop( )
    {
       try
       {
         aud_player.stop();
       }
       catch(MediaException eax)
       {
           
       }
    }
}
