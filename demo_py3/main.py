# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
#pip安装 pip install pycryptodome
from Crypto.Hash import MD2



# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    #print_hi('PyCharm')
    txt = "39"
    # md2加密1亿次还是2亿此根据题目要求
    for i in range(200000000):
        txt = MD2.new(txt.encode("utf8")).hexdigest()
    print(txt)
#17130526923
# See PyCharm help at https://www.jetbrains.com/help/pycharm/


