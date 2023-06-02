import { request } from './request';
import * as protoc from './ccallgate';
import * as protos from './gatecallc';
import * as common from './common';

export class PageData {
    public token : string = "";
    public account : string = "";
    public name : string = "name";
    public coin : number = 0;
    public level : number = 0;
    public current_img_page : number = 0;
    public current_img_guid : number = 0;
    public img_list : common.user_img[] = [];
  }
  
export function requestPageData(login_uri:string, req:any) : Promise<PageData> {
    return new Promise<PageData>((resolve, reject) => {
      request<protos.user_data_rsp>(login_uri, req).then((user_data)=>{
        if (user_data.code != common.error.success) {
          reject(user_data.code);
        }
        else {
          let rdata : PageData = new PageData();
          rdata.token = user_data.token;
          if (user_data.data) {
            rdata.account = user_data.data.account;
            rdata.name = user_data.data.name;
            rdata.coin = user_data.data.coin;
            rdata.level = user_data.data.level;
          }
    
          console.log("user_data");
          console.log(user_data);
            
          req = new protoc.user_query_home_req();
          req.token = rdata.token;
          req.last_img_guid = rdata.current_img_guid;
          request<protos.user_query_img_rsp>('query_home', req).then((img_data)=>{
    
            console.log("img_data");
            console.log(img_data);
    
            rdata.current_img_guid = img_data.current_img_guid;
            rdata.img_list = img_data.img_list;
            
            resolve(rdata);
          });
        }
      });
    });
  }
  