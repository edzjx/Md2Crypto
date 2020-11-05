# 此项目来源一个字谜解体过程

一个程序猿在自己的微信公众号里出了一个字谜。其中用到了MD2加密算法，这是各很古老的加密算法。从网上搜到作者92年发布的C代码还能正常执行。此项目介绍解题过程，和使用C，C#,Java,Python3来测试代码。

## 文章结构

1.  破题
2.  解体代码
3.  项目代码说明
4.  参考引用

## 题目：黑白皆算，对我等众猿而言中央C所在位置数优剃爱肤杠吧爱慕帝贰亿次的值是？

# 解：

### 1.破题

**1.1黑白皆算，中央C所在位置（C do）**

![](https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1604315012886&di=59250dec35213b71cfa9476642a2e088&imgtype=0&src=http%3A%2F%2Fpic2.zhimg.com%2Fv2-8d61f9021716d2311585b15b9c1a60b9_1200x500.jpg)

上图是一个标准的钢琴键盘示意图。这句话的意思黑键也算，从第一个数中央C是第40个（每组12按键（7个白键+5个黑键，第一组前有3个按键）。

**1.2 对我等众猿而言**

意思是从0开始索引，那么C do的位置值是39

**1.3优剃爱肤杠吧 爱慕帝贰 贰亿次 的值**

这句话是谐音，实际指的的是 UTF-8 MD2 2亿次的值。MD2是历史悠久一个Hash加密算法，最初用于8位机这种嵌入式设备上，目前其安全性很低，不建议使用了。建议参考MD2算法作者原文上的测试结果，来验证自己语言的加密算法。（网上搜到其作者92年写的算法原文上面的C代码我直接拷贝到clan中也照样执行），Hash值这类算法一般输入是一个字节数组。所以UTF-8的意思，以UTF-8编码格式获取39的字节编码。2亿次的意思就说循环加密2亿次。实际上UTF-8对于此题没有意义，对于0x00-0x7F之间的字符（包含数字字母），UTF-8编码与ASCII编码完全相同。

破题后就是写代码来计算了。

### 2.解题代码

**2.1Python**

Python的代码最为简洁，我参考52pojie贴子：

from Crypto.Hash import MD2

if \_\_name\_\_ == '\_\_main\_\_':  
#print_hi('PyCharm')  
txt = "39"  
\# md2加密1亿次  
for i in range(200000000):  
txt = MD2.new(txt.encode("utf8")).hexdigest()  
print(txt)

需要Python环境安装pycryptodome ,使用pip安装 pip install pycryptodome。hexdigest()作为十六进制数据字符串值

**2.2Java**

需要导入apache.commons.codec库。我建议建立Maven项目。这样方便在线导入依赖。否则手工需要去官网下载jar包，再导入jar包。代码书写起来简洁程度不亚于Python。

package com.company;  
import org.apache.commons.codec.digest.DigestUtils;  
public class Main {  
public static void main(String\[\] args) {  
String hash="";  
String txt ="39";

```
    // MD2  
    for(int i=0;i<200000000;i++){  
        ///   public static byte\[\] md2(String data) {  
        //        return md2(StringUtils.getBytesUtf8(data));  
        //    }  
        hash = DigestUtils.md2Hex(txt);  
    }  

    System.out.println(hash);  

}  

```

}

**2.3CSharp**

C#的代码最为麻烦，首先C#官方库（[System.Security.Cryptography](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography?view=netframework-4.8)）只有MD5加密算法，另外第三方收费库Chilkat .NET包含MD2算法，但是价格太贵。还好Mono中有。需要通过Nuget在项目里安装。再者C#加密类调用步骤都多一些，而且没有直接字符输出的功能。我这对此封装了方法，一个是进行字符串输出，一个是封装加密计算过程。使得最终调用的风格和上述两个代码类似。

新建项目后，NutGet 搜索Mono.Security安装。

using System;  
using System.Text;  
using Mono.Security.Cryptography;

namespace testdemo  
{  
class Program  
{  
static void Main(string\[\] args)  
{  
string txt = "39";  
for (int i = 0; i < 200000000; i++)  
{  
txt = Md2Crypto(txt);  
Console.WriteLine($"{i:d9}:{txt}");  
}  
}

```
    public static string Md2Crypto(string source)  
    {  
        using (MD2 myMD2 = MD2.Create())  
        {  
            try  
            {  
                byte\[\] input = Encoding.UTF8.GetBytes(source);  
                byte\[\] output=  myMD2.ComputeHash(input);  

                //string hashstr = GetHexStrByteArray(output);                     
                return hashstr;  

            }  
            catch (Exception e)  
            {  
                Console.WriteLine(e);  
                throw;          
            }  
        }  
    }  
    /// &lt;summary&gt;  
    /// 官方文档介绍为了hash后是需要核实后位十六进制字节数组，为了方便查看结果，格式化成2位十六禁止位的字符串  
    /// https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.md5?view=netframework-4.8  
    /// ComputeHash类的方法将 MD5 哈希值作为16字节的数组返回。 请注意，某些 MD5 实现产生了32字符的十六进制格式的哈希。  
    /// 若要与此类实现进行互操作，请将方法的返回值格式化 ComputeHash 为十六进制值。  
    /// &lt;/summary&gt;  
    /// &lt;param name="array"&gt;&lt;/param&gt;  
    /// &lt;returns&gt;&lt;/returns&gt;  
    public static string GetHexStrByteArray(byte\[\] array)  
    {  
        string result = "";  
        for (int i = 0; i < array.Length; i++)  
        {  
            //输出2位的十六进制  
            result+=($"{array\[i\]:x2}");                   
        }  
        return result;  
    }  
}  

```

}

## 3.测试代码项目说明

### 3.1 MD2Hash_C

**环境:**

- IDE:CLion
- 构建CMake(3.17)
- 编译器MSVC14.27

**目录：**

-           src  *#C源码目录 *
-            ----md2.c  *#md2算法实现*
-            ----mddriver.c  *#测试代码Main()在此文件在*
-            include *#头文件目录*
-            ----global.h *#全局配置文件*
-            ----md2.h *#md2.c头文件*

### 3.2 demo_py3

**环境：**

- IDE:pycharm
- 构建：IDE内置
- py3环境：vs2019自带python环境 py3.7 x64

**目录：**

- main.py *#测试源代码*

### 3.3 demo_Java

- IDE:IDEA
- 构建：IDE内置
- Java环境：java1.8 

目录：

- lib 第三方库目录
- ----commons-codec-1.15.java  *#引用的MD2加密算法库*
- src 源代码文件
- ----com.company.Main *#测试源代码*

3.4 demo_c#

- IDE:Rider
- 构建：IDE内置
- Dotnet环境：netcore3.1

目录：

- testdemo
- ----Program.cs *#测试源代码*

## 参考引用

【1】MD2最初算法（C实现）[The MD2 Message-Digest Algorithm](https://www.ietf.org/rfc/rfc1319.txt)

【2】[52pojie提问帖 读书成诗-谜题求解2](https://www.52pojie.cn/forum.php?mod=viewthread&tid=1288666) 第91楼

【3】[Java 加密库官网 Apache Commons Codec](https://commons.apache.org/proper/commons-codec/)

【4】参考网上的java MD2 demo Java [MD2加密算法](http://www.what21.com/sys/view/java_java-secure_1456896048512.html)

【6】使用Mono加密库官网介绍 [Cryptography](https://www.mono-project.com/archived/cryptography/#assembly-monosecuritywin32)

【7】参考CSharp Cryptography库使用代码（此网站可以搜索代码很方便）  [C# (CSharp) Mono.Security.Cryptography MD2.ComputeHash Examples](https://csharp.hotexamples.com/examples/Mono.Security.Cryptography/MD2/ComputeHash/php-md2-computehash-method-examples.html)

【8】参考MSDN MD5官方文档 [MD5 类](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.md5?view=netframework-4.8) 代码参考的 [SHA256 类](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.sha256?view=netframework-4.8)

【9】[python3中digest()和hexdigest()区别](https://www.cnblogs.com/yrxns/p/7727471.html)

【10】 [字符编码之ASCII、UTF-8、UTF-16的区别](https://blog.csdn.net/adminlpx/article/details/79304078)