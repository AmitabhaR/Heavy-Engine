
#ifndef NETWORK_MANAGER_H

#include<SDL2/SDL_net.h>
#include "HExtra.h"
#include "GameObject_Scene.h"

#define MESSAGE_FLAG_SERVER_ONLY 0xA
#define MESSAGE_FLAG_EVERYONE 0xB
#define CONNECTION_SERVER 0xC
#define CONNECTION_CLIENT 0xD
#define HANDLER_MESSAGE 0x7
#define HANDLER_UPDATE_GAMEOBJECT 0x10
#define HANDLER_DESTROY_GAMEOBJECT 0x12
#define HANDLER_CREATE_GAMEOBJECT 0x51
#define HANDLER_DISCONNECT_PLAYER 0x61
#define HANDLER_CONNECTED 0x30

struct NetworkPlayer
{
	IPaddress ip;
	TCPsocket socket;
};

typedef void (*OnConnected)(NetworkPlayer *);
typedef void (*OnMessageReceived)(char * , int );
typedef void (*OnUpdateGameObject)(GameObject_Scene *);
typedef void (*OnDestroyGameObject)(GameObject_Scene *);
typedef void (*OnCreateGameObject)(GameObject_Scene *);
typedef void (*OnPlayerDisconnected)(NetworkPlayer *);

class NetworkManager
{
public:

	static int getConnectionType();

	static bool startServer( int );

	static bool isConnected();

	static NetworkPlayer getNetworkPlayer();

	static std::list<NetworkPlayer> getConnectedPlayers();

	static bool connectServer( std::string , int );

	static void sendMessage( std::string , int );

	static void updateGameObject( GameObject_Scene * );

	static void destroyGameObject( GameObject_Scene * );

	static void createGameObject( GameObject_Scene * );

	static void updateNetwork();

	static void addSignalHandler( int , void * );

	static void disconnect();

};

#endif
