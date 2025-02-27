#include <iostream>
#include "crow.h"

using namespace std;

int main()
{
    crow::SimpleApp app;

    CROW_ROUTE(app, "/generate-text")
    ([&]() {
        // Simulate heavy computation
        std::this_thread::sleep_for(std::chrono::seconds(5));
        return "Task completed!";
    });

    app.port(8081).multithreaded().run();
}
