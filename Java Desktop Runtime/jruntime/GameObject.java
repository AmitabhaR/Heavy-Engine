package jruntime;

import java.awt.Color;
import java.awt.image.BufferedImage;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class GameObject
{
    public String name;
    public String text;
    public int tag;
    public BufferedImage img;
    public boolean _static;
    public boolean physics;
    public boolean rigidbody;
    public boolean collider;
    public String font_name = "Verdana";
    public int font_size = 12;
    public Color color = Color.BLACK;
}
