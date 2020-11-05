# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.

from Crypto.Hash import MD2
def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press Ctrl+F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    #print_hi('PyCharm')
    txt = "39"
    # md2加密1亿次
    for i in range(100000000):
        txt = MD2.new(txt.encode("utf8")).hexdigest()
    print(txt)
17130526923
# See PyCharm help at https://www.jetbrains.com/help/pycharm/


