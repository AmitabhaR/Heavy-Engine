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
public abstract class OnMessageReceived 
{
    public abstract void processMessageReceived(byte[] buffer,int buffer_size);
}
