/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

/**
 *
 * @author Riju
 */
public abstract class TouchHandler 
{
    public abstract void onTouch( int x , int y );
    public abstract void onTouchReleased( int x , int y );
    public abstract void onTouchMoved(int x , int y);
}
