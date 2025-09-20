#include <string>
#include <iostream>
#include <filesystem>
#include <fstream>

namespace fs = std::filesystem;

int main() {
        std::string path = "/sys/bus/w1/devices";

        for(const auto& entry : fs::directory_iterator(path)) {
                std::string path = entry.path().string();
                if(path.find("28-") != path.npos) {
                        std::ifstream f(path + "/w1_slave");
                        if(f.is_open()) {
                                std::string tmp;
                                while(getline(f, tmp)) {
                                        auto match = tmp.find("t=");
                                        if(match != tmp.npos) {
                                                double read = std::stod(tmp.substr(match + 2)) / 1000;
						std::cout << read;
                                        }
                                }
                                f.close();
                        }
                        else {
                                std::cout << 0;
                        }
                }
        }
}

