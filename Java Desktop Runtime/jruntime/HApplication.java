package jruntime;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseMotionListener;
import java.awt.event.MouseWheelListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.util.ArrayList;
import javax.swing.*;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */

      public class HApplication
      {
        static Scene cur_scene;
        static JFrame mainWindow;
        static HCanvas canvas;
        static ArrayList keys_handler = new ArrayList( );
        static int mouseX = 0;
        static int mouseY = 0;
        static ArrayList mouseHandlers = new ArrayList( );
        
        public static void loadScene(Scene newScene)
        {
            cur_scene = newScene;
            canvas.setScene(cur_scene);
        }

        public static Scene getActiveScene()
        {
            return cur_scene;
        }

        public static void setSize(int width, int height)
        {
            mainWindow.setSize(width , height );
        }

        public static int[] getSize()
        {
            int[] size = { mainWindow.getWidth( ), mainWindow.getHeight( ) };
            return size;
        }

        public static JFrame getWindowHandle()
        {
            return mainWindow;
        }
          
        public static void Initialize(String project_name)
        {
            // Initialize and load stuffs.
            mainWindow = new JFrame(project_name);
            canvas = new HCanvas( null );
            canvas.setLocation(0, 0); 
            canvas.setSize(1000, 1000);
            
            mainWindow.addWindowListener(new WindowAdapter( ){
                
                @Override
                public void windowClosing(WindowEvent e)
                {
                    System.exit(1);
                }
                
            });
            
            mainWindow.addKeyListener(new KeyListener()
            {
                @Override
                public void keyReleased(KeyEvent e)
                {
                    
                }
                
                @Override
                public void keyPressed(KeyEvent e)
                {
                    for(int cnt = 0;cnt < keys_handler.size();cnt++)
                    {
                        ((KeyboardHandler) keys_handler.get(cnt)).onKeyPress(e.getKeyCode());
                    }
                }
                
                @Override
                public void keyTyped(KeyEvent e)
                {
                    
                }
            });
            
            mainWindow.addMouseListener(new MouseListener(){
                
                @Override
                public void mouseExited(MouseEvent e)
                {
                    
                }
                
                @Override
                public void mouseEntered(MouseEvent e)
                {
                    
                }
                
                @Override
                public void mouseReleased(MouseEvent e)
                {
                    
                }
                
                @Override
                public void mousePressed(MouseEvent e)
                {
                    
                }
                
                @Override
                public void mouseClicked(MouseEvent e)
                {
                    for(int cnt = 0;cnt < mouseHandlers.size();cnt++)
                    {
                        ((MouseHandler) mouseHandlers.get(cnt)).onMouseDown(e.getButton());
                    }
                }
                
            });
            
            mainWindow.addMouseMotionListener(new MouseMotionListener(){
                
                @Override
                public void mouseMoved(MouseEvent e)       
                {
                    mouseX = e.getX();
                    mouseY = e.getY( );
                }
                
                @Override
                public void mouseDragged(MouseEvent e)
                {
                    
                }
            });
            
            mainWindow.setResizable(false);
            mainWindow.setUndecorated(true);
            mainWindow.setSize(1000, 1000);
            mainWindow.add(canvas);
            mainWindow.setVisible(true);
        }

        public static HCanvas getCanvas()
        {
            return canvas;
        }
        
        public static int[] getMousePosition()
        {
            int[] pos = {mouseX , mouseY };
            
           return pos;
        }
        
        public void registerKeyHandler( KeyboardHandler handler )
        {
             keys_handler.add(handler);
        }
        
        public void registerMouseHandler(KeyboardHandler handler)
        {
            mouseHandlers.add(handler);
        }
    }
