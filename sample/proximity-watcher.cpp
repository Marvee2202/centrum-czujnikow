#include <iostream>
#include <chrono>
#include <thread>
#include <wiringPi.h>

#define PROXIMITY_TRIGGER 24
#define PROXIMITY_ECHO 25

using namespace std::chrono_literals;

int main() {
	wiringPiSetup();
	pinMode(PROXIMITY_TRIGGER, OUTPUT);
	pinMode(PROXIMITY_ECHO, INPUT);

	digitalWrite(PROXIMITY_TRIGGER, HIGH);
	delay(0.01);
	digitalWrite(PROXIMITY_TRIGGER, LOW);

	while(digitalRead(PROXIMITY_ECHO) == 0) {
		std::this_thread::sleep_for(0.001ms);
	}

	std::chrono::steady_clock::time_point begin = std::chrono::steady_clock::now();

	while(digitalRead(PROXIMITY_ECHO) == 1) {
		std::chrono::steady_clock::time_point end = std::chrono::steady_clock::now();
        	if (std::chrono::duration_cast<std::chrono::nanoseconds>(end - begin).count() > 25000000) {
			std::cout << -1;
			return 0;
		}
		std::this_thread::sleep_for(0.001ms);
	}

	std::chrono::steady_clock::time_point end = std::chrono::steady_clock::now();
	double time = std::chrono::duration_cast<std::chrono::nanoseconds>(end - begin).count();
	double distance = time * 17.15 / 1000000;

	std::cout << distance;
	return 0;
}
