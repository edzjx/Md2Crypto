cmake_minimum_required(VERSION 3.17)
project(MD2Hash)

set(CMAKE_CXX_STANDARD 14)

add_executable(MD2Hash include/global.h include/md2.h src/mddriver.c src/md2.c)