package jruntime;

import javax.swing.*;
import java.awt.*;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class HCanvas extends JPanel 
{
    Scene sceneHandle;
    
    public HCanvas(Scene sceneInstance)
    {
        sceneHandle = sceneInstance;
    }
    
    public void setScene(Scene sceneInstance )
    {
          sceneHandle = sceneInstance;
    }
    
    @Override
    public void paint(Graphics g)
    {
    //    super.paint(g);
        
        if (sceneHandle != null)
        {
             sceneHandle.drawScene(g);
        }
        
       repaint( );
    }
}
