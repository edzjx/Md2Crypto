package com.company;

import org.apache.commons.codec.digest.DigestUtils;

public class Main {


    public static void main(String[] args) {
        String hash="";
        String txt ="39";

        // MD2
        for(int i=0;i<100000000;i++){
            ///   public static byte[] md2(String data) {
            //        return md2(StringUtils.getBytesUtf8(data));
            //    }
            hash = DigestUtils.md2Hex(txt);
        }

        System.out.println(hash);

    }
}
