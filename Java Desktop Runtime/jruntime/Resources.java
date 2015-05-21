package jruntime;

import java.io.*;
import java.awt.image.*;
import javax.imageio.ImageIO;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class Resources
{
	public static BufferedImage findImage(String file_name)
	{
            File fl = new File("/Data/" + file_name + ".X");
            
            if (fl.exists() && !fl.isDirectory())
            {
                try
                {
                    return ImageIO.read(fl);
                }
                catch(IOException ax)
                {
                    return null;
                }
            }
            else
            {
		return null;
            }
	}
		
	public static InputStream getResourceAsStream(String file_name)
	{
            File fl = new File("/Data/" + file_name + ".X");
            
	    if (fl.exists() && !fl.isDirectory())
	    {
                try
                {
                    return new FileInputStream("/Data/" + file_name + ".X");
                }
                catch ( FileNotFoundException ax )
                {
                    return null;
                }
	    }
	    else
	    {
                return null;
            }
	}
		
	public static String getResource(String file_name)
	{
            File fl = new File( "/Data/" + file_name + ".X");
            
		if (fl.exists() && !fl.isDirectory())
		{
                    return "/Data/" + file_name + ".X";
		}
		else
		{
                    return "";
		}
	}
}
