/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import javazoom.jl.decoder.JavaLayerException;
import javazoom.jl.player.*;
import java.io.*;

/**
 *
 * @author Riju
 */
public class Audio implements Runnable
{
    private int audio_type;
    private Player audio_player;
    private int status = 0;
    private long player_pos;
    private String file_name;
    
    public Audio(String name)
    {
        try
        {    
            file_name = name;
            this.audio_player = new Player(ResourceManager.getResourceAsStream(name));
        }
        catch(JavaLayerException ex)
        {
            
        }
    }
    
    public void play()
    {
        if (status == 0x2)
        {
            InputStream stm = ResourceManager.getResourceAsStream(file_name);
            try { stm.skip(player_pos);
            audio_player = new Player(stm);
            Thread t = new Thread(this);
            t.start();
            
             } catch (IOException ex) {} catch (JavaLayerException bx){ }
        }
        else
        {
            if (audio_player == null) return;
            Thread t = new Thread(this);
            t.start();
        }
    }
    
    public void stop() 
    { 
        if (audio_player == null) return; 
        if (status == 0x2 || status == 0x1) audio_player.close(); 
    }
    
    public boolean isPlaying()
    {
        return (status == 0x1 && !audio_player.isComplete());
    }
    
    public void pause()
    {
        if (audio_player == null) return;
        player_pos = audio_player.getPosition();
        audio_player.close();
        status = 0x2;
    }
    
    @Override
    public void run()
    {
        try
        {
            status = 0x1;
            audio_player.play();
        }
        catch(JavaLayerException ex)
        {
            status = 0x0;
        }
    }
}
