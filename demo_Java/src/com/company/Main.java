package com.company;

import org.apache.commons.codec.digest.DigestUtils;

public class Main {


    public static void main(String[] args) {
        
        String txt ="39";

        // MD2  ������ĿҪ��2�ڻ���1��
        for(int i=0;i<200000000;i++){
            ///   public static byte[] md2(String data) {
            //        return md2(StringUtils.getBytesUtf8(data));
            //    }
            txt = DigestUtils.md2Hex(txt);
        }

        System.out.println(txt);

    }
}
