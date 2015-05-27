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

std::string ResourceManager::decryptFileName(std::string base_string)
{
	std::string out_string = "";
	int skip_count = 0;

	if (base_string == "")
	{
		return "";
	}

	for (int cnt = 0; cnt < base_string.length(); cnt++)
	{
		if (skip_count > 0 && skip_count <= 3)
		{
			skip_count++;
			continue;
		}
		else
		{
			skip_count = 0;
		}

		char cur_ch = base_string[cnt];
	
		if ((bool)::isalpha(cur_ch) )
		{
			if (base_string[cnt - 1] == '0')
			{
				out_string += (char)(cur_ch - 10);
			}
			else
			{
				out_string += (char)(cur_ch + 10);
			}

			skip_count = 1;
		}
		else
		{
			if (cnt == 0) continue;

			if (base_string[cnt - 1] == '2')
			{
				out_string += cur_ch; // Avoid Digits.
				skip_count = 3;
			}
		}
	}

	return out_string;
}

std::string ResourceManager::encryptFileName(std::string base_string)
{
	std::string out_string = "";

	if (base_string == "")
	{
		return "";
	}

	for (int cnt = 0; cnt < base_string.length(); cnt++)
	{
		if (base_string[cnt] >= 97 || base_string[cnt] <= 123)
		{
			base_string[cnt] = 65 + (base_string[cnt] - 97);
		}
	}

	for (int cnt = 0; cnt < base_string.length(); cnt++)
	{
		char cur_ch = base_string[cnt];

		if (cur_ch < 65 || cur_ch > 91)
		{
			out_string += (int) 2;
			out_string += cur_ch; // Avoid Digits.
		}
		else if (cur_ch + 10 < 91)
		{
			out_string += (char)'0';
			out_string += (char)(cur_ch + 10);
			out_string += (int)(91 - (cur_ch + 10));

			if (91 - (cur_ch + 10) < 10)
			{
				out_string += (char)'0';
			}
		}
		else if (cur_ch - 10 >= 65)
		{
			out_string += (int)1;
			out_string += (char)(cur_ch - 10);
			out_string += (int)((cur_ch - 10) - 65);

			if ((cur_ch - 10) - 65 < 10)
			{
				out_string += (char)'0';
			}
		}
	}

	//  out_string = out_string.toUpperCase();

	return out_string;
}