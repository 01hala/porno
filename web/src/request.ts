import axios from 'axios'
import { encode, decode } from "./@msgpack/msgpack";

export function request<T>(uri:string, req:any) : Promise<T> {
    return new Promise<T>((resolve, reject) => {
        axios.post(uri, encode(req), {
            headers: {
                "Content-Type": "application/msgpack",
                "Access-Control-Allow-Origin": "*",
            },
            responseType: 'arraybuffer',
            timeout: 20 * 1000
        }).then((res) => {
            console.log(res);
            resolve(decode(res.data) as T);
        }).catch(error => {
            console.error(error)
            if (error.message.includes('timeout')) {
                reject({data: null, err: "timeout"});
                return
            }
            reject({data: null, err: error.message});
        })
    });
}

