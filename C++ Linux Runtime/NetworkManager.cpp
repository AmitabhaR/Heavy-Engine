
#include "HApplication.h"
#include "NetworkManager.h"
#include "ObjectManager.h"

#define BUFFER_MESSAGE 0x7
#define BUFFER_UPDATE_GAMEOBJECT 0x10
#define BUFFER_CREATE_GAMEOBJECT 0x51
#define BUFFER_DISCONNECT_PLAYER 0x61

static NetworkPlayer basePlayer;
static std::list<NetworkPlayer> client_list;
static std::list<OnConnected> onConnected_handler_list;
static std::list<OnCreateGameObject> onCreate_handler_list;
static std::list<OnDestroyGameObject> onDestroy_handler_list;
static std::list<OnMessageReceived> onMessage_handler_list;
static std::list<OnPlayerDisconnected> onDisconnected_handler_list;
static std::list<OnUpdateGameObject> onUpdate_handler_list;
static bool isNetworkReady = false;
static int user_type = 0;
static SDLNet_SocketSet sockets;

int NetworkManager::getConnectionType() { return user_type; }
NetworkPlayer NetworkManager::getNetworkPlayer() { return basePlayer; }
bool NetworkManager::isConnected() { return isNetworkReady;  }

std::list<NetworkPlayer> NetworkManager::getConnectedPlayers()
{
	if (user_type == CONNECTION_SERVER) return client_list;

	return std::list<NetworkPlayer>();
}

bool NetworkManager::startServer(int port)
{
	if (!SDLNet_ResolveHost(&basePlayer.ip, NULL, port))
	{
		if ((basePlayer.socket = SDLNet_TCP_Open(&basePlayer.ip)))
		{
			user_type = CONNECTION_SERVER;
			isNetworkReady = true;
			return true;
		}
	}
	
	basePlayer = NetworkPlayer();

	return false;
}

bool NetworkManager::connectServer(std::string ip, int port)
{
	if (!SDLNet_ResolveHost(&basePlayer.ip, ip.c_str(), port))
	{
		if ((basePlayer.socket = SDLNet_TCP_Open(&basePlayer.ip)))
		{
			user_type = CONNECTION_CLIENT;
			isNetworkReady = true;
			
			sockets = SDLNet_AllocSocketSet(1);

			SDLNet_TCP_AddSocket(sockets, basePlayer.socket);

			return true;
		}
	}

	basePlayer = NetworkPlayer();

	return false;
}

static int ReadInteger(char * buffer)
{
	union IntegerBreak
	{
		int value;
		char bytes[4];
	} breaker;

	breaker.value = 0;

	for (int cnt = 0; cnt < 4; cnt++)
	{
		breaker.bytes[cnt] = buffer[cnt];
	}

	return breaker.value;
}

void NetworkManager::updateNetwork()
{
	if (!isNetworkReady) return;

	char * data_buffer = new char[512]; // Allocate 512 bytes on dynamic memory.

	if (user_type == CONNECTION_SERVER)
	{
		// Server Part.
		NetworkPlayer newPlayer;

		if ((newPlayer.socket = SDLNet_TCP_Accept(basePlayer.socket))) // Check for new players.
		{
			IPaddress * ip;

			if ((ip = SDLNet_TCP_GetPeerAddress(newPlayer.socket)))
			{
				newPlayer.ip = *ip;
				client_list.push_back(newPlayer);

				if (sockets) SDLNet_FreeSocketSet(sockets);
				sockets = SDLNet_AllocSocketSet(client_list.size());

				for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
				{
					SDLNet_TCP_AddSocket(sockets, cur_cntr->socket);
				}

				for (std::list<OnConnected>::iterator cur_cntr = onConnected_handler_list.begin(); cur_cntr != onConnected_handler_list.end(); cur_cntr++)
				{
					(*cur_cntr)(&newPlayer);
				}
			}
		}

		std::list<NetworkPlayer>::iterator cur_counter = client_list.begin();
		
		int num_ready = -1; 
		if (sockets) num_ready = SDLNet_CheckSockets(sockets, 100);

		if (num_ready == -1 || !num_ready) { delete [] data_buffer; return; }

		for (; cur_counter != client_list.end(); cur_counter++) // Check all the ports.
		{
			if (!SDLNet_SocketReady(cur_counter->socket)) continue;

			if (SDLNet_TCP_Recv(cur_counter->socket, data_buffer, 512) > 0)
			{
				if (*data_buffer == BUFFER_MESSAGE)
				{
					unsigned int buffer_size = ReadInteger(data_buffer + 1);
					char flag = data_buffer[5];

					if (flag == MESSAGE_FLAG_EVERYONE)
					{
						for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
						{
							if (cur_cntr != cur_counter)
							{
								SDLNet_TCP_Send(cur_cntr->socket, (void *)data_buffer, 6 + buffer_size);
							}
						}
					}

					for (std::list<OnMessageReceived>::iterator cur_cntr = onMessage_handler_list.begin(); cur_cntr != onMessage_handler_list.end(); cur_cntr++)
					{
						(*cur_cntr)(data_buffer + 6, buffer_size);
					}
				}
				else if (*data_buffer == BUFFER_DISCONNECT_PLAYER)
				{
					for (std::list<OnPlayerDisconnected>::iterator cur_cntr = onDisconnected_handler_list.begin(); cur_cntr != onDisconnected_handler_list.end(); cur_cntr++)
					{
						#define cur_player newPlayer

						cur_player = *cur_counter;

						(*cur_cntr)(&cur_player);
						
						#undef cur_player
					}

					client_list.erase(cur_counter);
					break;
				}
				else if (*data_buffer == BUFFER_CREATE_GAMEOBJECT)
				{
					char * object_name = data_buffer + 1;
					char * baseobj_name = object_name + strlen(object_name) + 1;
					int pos_x = ReadInteger(baseobj_name + strlen(baseobj_name) + 1);
					int pos_y = ReadInteger(baseobj_name + strlen(baseobj_name) + 5);
					int depth = ReadInteger(baseobj_name + strlen(baseobj_name) + 9);
					GameObject_Scene * gameObject = new GameObject_Scene;

					gameObject->instance_name = object_name;
					gameObject->obj_instance = ObjectManager::findGameObjectWithName(baseobj_name);
					gameObject->pos_x = pos_x;
					gameObject->pos_y = pos_y;
					gameObject->depth = depth;
					gameObject->isDestroyed = false;

					HApplication::getActiveScene()->loadGameObject(gameObject);

					for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
					{
						if (cur_cntr != cur_counter)
						{
							SDLNet_TCP_Send(cur_cntr->socket, (void *)data_buffer, (strlen(object_name) + 1) + (strlen(baseobj_name) + 1) + 12);
						}
					}

					for (std::list<OnCreateGameObject>::iterator cur_cntr = onCreate_handler_list.begin(); cur_cntr != onCreate_handler_list.end(); cur_cntr++)
					{
						(*cur_cntr)(gameObject);
					}
				}
				else if (*data_buffer == BUFFER_UPDATE_GAMEOBJECT)
				{
					char * object_name = data_buffer + 1;
					char * baseobj_name = object_name + strlen(object_name) + 1;
					int pos_x = ReadInteger(baseobj_name + strlen(baseobj_name) + 1);
					int pos_y = ReadInteger(baseobj_name + strlen(baseobj_name) + 5);
					int depth = ReadInteger(baseobj_name + strlen(baseobj_name) + 9);
					bool * isDestroyed = (bool *) baseobj_name + strlen(baseobj_name) + 13;

					GameObject_Scene * gameObject;

					if ((gameObject = HApplication::getActiveScene()->findGameObject(object_name)))
					{
						if (*isDestroyed == true)
						{
							HApplication::getActiveScene()->destroyGameObject(object_name);

							for (std::list<OnDestroyGameObject>::iterator cur_cntr = onDestroy_handler_list.begin(); cur_cntr != onDestroy_handler_list.end(); cur_cntr++)
							{
								(*cur_cntr)(gameObject);
							}
						}
						else
						{
							gameObject->instance_name = object_name;
							gameObject->obj_instance = ObjectManager::findGameObjectWithName(baseobj_name);
							gameObject->pos_x = pos_x;
							gameObject->pos_y = pos_y;
							gameObject->depth = depth;
							gameObject->isDestroyed = false;

							for (std::list<OnUpdateGameObject>::iterator cur_cntr = onUpdate_handler_list.begin(); cur_cntr != onUpdate_handler_list.end(); cur_cntr++)
							{
								(*cur_cntr)(gameObject);
							}
						}
					}

					for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
					{
						if (cur_cntr != cur_counter)
						{
							SDLNet_TCP_Send(cur_cntr->socket, (void *)data_buffer, (strlen(object_name) + 1) + (strlen(baseobj_name) + 1) + 13);
						}
					}
				}

				data_buffer = new char[512];
			}
		}
	}
	else
	{
		// Client Part.
		int num_ready = -1;
		if (sockets) num_ready = SDLNet_CheckSockets(sockets, 100);

		if (num_ready == -1 || !num_ready) { delete[] data_buffer; return; }

		if (!SDLNet_SocketReady(basePlayer.socket)) return;

		if (SDLNet_TCP_Recv(basePlayer.socket, data_buffer, 512) > 0)
		{
			if (*data_buffer == BUFFER_MESSAGE)
			{
				unsigned int buffer_size = ReadInteger(data_buffer + 1);
				
				for (std::list<OnMessageReceived>::iterator cur_cntr = onMessage_handler_list.begin(); cur_cntr != onMessage_handler_list.end(); cur_cntr++)
				{
					(*cur_cntr)(data_buffer + 6, buffer_size);
				}
			}
			else if (*data_buffer == BUFFER_CREATE_GAMEOBJECT)
			{
				char * object_name = data_buffer + 1;
				char * baseobj_name = object_name + strlen(object_name) + 1;
				int pos_x = ReadInteger(baseobj_name + strlen(baseobj_name) + 1);
				int pos_y = ReadInteger(baseobj_name + strlen(baseobj_name) + 5);
				int depth = ReadInteger(baseobj_name + strlen(baseobj_name) + 9);
				GameObject_Scene * gameObject = new GameObject_Scene;

				gameObject->instance_name = object_name;
				gameObject->obj_instance = ObjectManager::findGameObjectWithName(baseobj_name);
				gameObject->pos_x = pos_x;
				gameObject->pos_y = pos_y;
				gameObject->depth = depth;
				gameObject->isDestroyed = false;

				HApplication::getActiveScene()->loadGameObject(gameObject);

				for (std::list<OnCreateGameObject>::iterator cur_cntr = onCreate_handler_list.begin(); cur_cntr != onCreate_handler_list.end(); cur_cntr++)
				{
					(*cur_cntr)(gameObject);
				}
			}
			else if (*data_buffer == BUFFER_UPDATE_GAMEOBJECT)
			{
				char * object_name = data_buffer + 1;
				char * baseobj_name = object_name + strlen(object_name) + 1;
				int pos_x = ReadInteger(baseobj_name + strlen(baseobj_name) + 1);
				int pos_y = ReadInteger(baseobj_name + strlen(baseobj_name) + 5);
				int depth = ReadInteger(baseobj_name + strlen(baseobj_name) + 9);
				bool * isDestroyed = (bool *)baseobj_name + strlen(baseobj_name) + 13;

				GameObject_Scene * gameObject;

				if ((gameObject = HApplication::getActiveScene()->findGameObject(object_name)))
				{
					if (*isDestroyed == true)
					{
						HApplication::getActiveScene()->destroyGameObject(object_name);

						for (std::list<OnDestroyGameObject>::iterator cur_cntr = onDestroy_handler_list.begin(); cur_cntr != onDestroy_handler_list.end(); cur_cntr++)
						{
							(*cur_cntr)(gameObject);
						}
					}
					else
					{
						gameObject->instance_name = object_name;
						gameObject->obj_instance = ObjectManager::findGameObjectWithName(baseobj_name);
						gameObject->pos_x = pos_x;
						gameObject->pos_y = pos_y;
						gameObject->depth = depth;
						gameObject->isDestroyed = false;

						for (std::list<OnUpdateGameObject>::iterator cur_cntr = onUpdate_handler_list.begin(); cur_cntr != onUpdate_handler_list.end(); cur_cntr++)
						{
							(*cur_cntr)(gameObject);
						}
					}
				}
			}
			else if (*data_buffer == BUFFER_DISCONNECT_PLAYER)
			{
				for (std::list<OnPlayerDisconnected>::iterator cur_cntr = onDisconnected_handler_list.begin(); cur_cntr != onDisconnected_handler_list.end(); cur_cntr++)
				{
					(*cur_cntr)(&basePlayer);
				}

				isNetworkReady = false;
				if (sockets) SDLNet_FreeSocketSet(sockets);
				SDLNet_TCP_Close(basePlayer.socket);
			}
		}
	}
}

void NetworkManager::addSignalHandler(int type, void * ptr)
{
	if (type == HANDLER_MESSAGE)
	{
		onMessage_handler_list.push_back((OnMessageReceived) ptr);
	}
	else if (type == HANDLER_CREATE_GAMEOBJECT)
	{
		onCreate_handler_list.push_back((OnCreateGameObject) ptr);
	}
	else if (type == HANDLER_DESTROY_GAMEOBJECT)
	{
		onDestroy_handler_list.push_back((OnDestroyGameObject)ptr);
	}
	else if (type == HANDLER_DISCONNECT_PLAYER)
	{
		onDisconnected_handler_list.push_back((OnPlayerDisconnected)ptr);
	}
	else if (type == HANDLER_UPDATE_GAMEOBJECT)
	{
		onUpdate_handler_list.push_back((OnUpdateGameObject)ptr);
	}
	else if (type == HANDLER_CONNECTED)
	{
		onConnected_handler_list.push_back((OnConnected)ptr);
	}
	
}

void NetworkManager::disconnect()
{
	if (isNetworkReady)
	{
		if (user_type == CONNECTION_SERVER)
		{
			for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
			{
				char data = BUFFER_DISCONNECT_PLAYER;

				SDLNet_TCP_Send(cur_cntr->socket, &data, 1);

				SDLNet_TCP_Close(cur_cntr->socket);
			}
		}
		else if (user_type == CONNECTION_CLIENT)
		{
			char data = BUFFER_DISCONNECT_PLAYER;

			SDLNet_TCP_Send(basePlayer.socket, &data, 1);
		}

		isNetworkReady = false;
		if (sockets) SDLNet_FreeSocketSet(sockets);
		SDLNet_TCP_Close(basePlayer.socket);
	}
}

static void LoadString(char * buffer , char * src)
{
	for (register int cnt = 0; cnt < strlen(src); cnt++)
	{
		*buffer++ = src[cnt];
	}

	*buffer++ = NULL;
}

static void LoadString(char ** buffer, char * src)
{
	for (register int cnt = 0; cnt < strlen(src); cnt++)
	{
		(*((char *)*buffer)) = src[cnt];
		(*buffer)++;
	}

	(*((char *) *buffer)) = NULL;
	(*buffer)++;
}

static void LoadInteger(char ** buffer, int value)
{
	union IntegerBreak
	{
		int value;
		char bytes[4];
	} breaker;

	breaker.value = value;

	for (int cnt = 0; cnt < 4; cnt++) { (*buffer)[cnt] = breaker.bytes[cnt]; }
	(*buffer) += 4;
}

void NetworkManager::createGameObject(GameObject_Scene * gameObject)
{
	if (!isNetworkReady) return;

	char * data_buffer = new char[512];
	char * send_buf_ptr = data_buffer;

	*data_buffer++ = BUFFER_CREATE_GAMEOBJECT;
	LoadString(&data_buffer, (char *)gameObject->instance_name.c_str());
	LoadString(&data_buffer, (char *)gameObject->obj_instance.name.c_str());
	LoadInteger(&data_buffer,gameObject->pos_x);
	LoadInteger(&data_buffer,gameObject->pos_y);
	LoadInteger(&data_buffer,gameObject->depth);

	if (user_type == CONNECTION_CLIENT)
	{
		SDLNet_TCP_Send(basePlayer.socket, (void *)send_buf_ptr, gameObject->instance_name.length() + gameObject->obj_instance.name.length() + 15);
	}
	else if (user_type == CONNECTION_SERVER)
	{
		for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
		{
			SDLNet_TCP_Send(cur_cntr->socket, (void *)send_buf_ptr, gameObject->instance_name.length() + gameObject->obj_instance.name.length() + 15);
		}
	}
}

void NetworkManager::updateGameObject(GameObject_Scene * gameObject)
{
	if (!isNetworkReady) return;

	char * data_buffer = new char[512];
	char * send_buf_ptr = data_buffer;

	*data_buffer++ = BUFFER_UPDATE_GAMEOBJECT;
	LoadString(&data_buffer, (char *)gameObject->instance_name.c_str());
	LoadString(&data_buffer, (char *)gameObject->obj_instance.name.c_str());
	LoadInteger(&data_buffer,gameObject->pos_x);
	LoadInteger(&data_buffer,gameObject->pos_y);
    LoadInteger(&data_buffer,gameObject->depth);
	*data_buffer++ = gameObject->isDestroyed;

	if (user_type == CONNECTION_CLIENT)
	{
		SDLNet_TCP_Send(basePlayer.socket, (void *)send_buf_ptr, gameObject->instance_name.length() + gameObject->obj_instance.name.length() + 15);
	}
	else if (user_type == CONNECTION_SERVER)
	{
		for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
		{
			SDLNet_TCP_Send(cur_cntr->socket, (void *)send_buf_ptr, gameObject->instance_name.length() + gameObject->obj_instance.name.length() + 15);
		}
	}
}

void NetworkManager::sendMessage(std::string message, int flag)
{
	if (!isNetworkReady) return;

	char * data_buffer = new char[512];
	char * send_buf_ptr = data_buffer;

	*data_buffer++ = BUFFER_MESSAGE;
	LoadInteger(&data_buffer,message.length());
	*data_buffer++ = flag;
	LoadString(&data_buffer, (char *) message.c_str());

	if (user_type == CONNECTION_CLIENT)
	{
		SDLNet_TCP_Send(basePlayer.socket, (void *)send_buf_ptr, message.length() + 7);
	}
	else if (user_type == CONNECTION_SERVER)
	{
		for (std::list<NetworkPlayer>::iterator cur_cntr = client_list.begin(); cur_cntr != client_list.end(); cur_cntr++)
		{
			SDLNet_TCP_Send(cur_cntr->socket, (void *)send_buf_ptr, message.length () + 7);
		}
	}
}