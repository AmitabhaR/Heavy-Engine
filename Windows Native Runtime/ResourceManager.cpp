#include "ResourceManager.h"

Image ResourceManager::findImage(std::string img_path)
{
	return IMG_Load(img_path.c_str());
}

std::string ResourceManager::getResource(std::string res_name)
{
	std::string res_file = "./Data/" + res_name;

	File fl = fopen(res_file.c_str(), "r");

	fclose(fl);

	if (fl)
	{
		return res_file;
	}

	return "";
}

File ResourceManager::getResourceAsStream(std::string file_name)
{
	if (getResource(file_name) != "")
	{
		return fopen(getResource(file_name).c_str(), "r");
	}
	else
	{
		return NULL;
	}
}