import * as common from "./common";
/*this enum code is codegen by abelkhan codegen for ts*/

/*this struct code is codegen by abelkhan codegen for typescript*/
export class guest_req
{
    public account : string = "";

}

export function guest_req_to_protcol(_struct:guest_req){
    return _struct;
}

export function protcol_to_guest_req(_protocol:any){
    let _struct = new guest_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "account"){
            _struct.account = val as string;
        }
    }
    return _struct;
}

export class user_login_req
{
    public account : string = "";
    public password : string = "";

}

export function user_login_req_to_protcol(_struct:user_login_req){
    return _struct;
}

export function protcol_to_user_login_req(_protocol:any){
    let _struct = new user_login_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "account"){
            _struct.account = val as string;
        }
        else if (key === "password"){
            _struct.password = val as string;
        }
    }
    return _struct;
}

export class user_create_req
{
    public token : string = "";
    public account : string = "";
    public password : string = "";
    public mail : string = "";
    public name : string = "";

}

export function user_create_req_to_protcol(_struct:user_create_req){
    return _struct;
}

export function protcol_to_user_create_req(_protocol:any){
    let _struct = new user_create_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "account"){
            _struct.account = val as string;
        }
        else if (key === "password"){
            _struct.password = val as string;
        }
        else if (key === "mail"){
            _struct.mail = val as string;
        }
        else if (key === "name"){
            _struct.name = val as string;
        }
    }
    return _struct;
}

export class free_order_req
{
    public token : string = "";
    public name : string = "";
    public prompt : string[] = [];

}

export function free_order_req_to_protcol(_struct:free_order_req){
    return _struct;
}

export function protcol_to_free_order_req(_protocol:any){
    let _struct = new free_order_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "name"){
            _struct.name = val as string;
        }
        else if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
    }
    return _struct;
}

export class payment_order_req
{
    public token : string = "";
    public name : string = "";
    public prompt : string[] = [];
    public type : common.coin_type = common.coin_type.coin;

}

export function payment_order_req_to_protcol(_struct:payment_order_req){
    return _struct;
}

export function protcol_to_payment_order_req(_protocol:any){
    let _struct = new payment_order_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "name"){
            _struct.name = val as string;
        }
        else if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
        else if (key === "type"){
            _struct.type = val as common.coin_type;
        }
    }
    return _struct;
}

export class user_payment_req
{
    public token : string = "";
    public order_uuid : string = "";
    public type : common.coin_type = common.coin_type.coin;

}

export function user_payment_req_to_protcol(_struct:user_payment_req){
    return _struct;
}

export function protcol_to_user_payment_req(_protocol:any){
    let _struct = new user_payment_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "order_uuid"){
            _struct.order_uuid = val as string;
        }
        else if (key === "type"){
            _struct.type = val as common.coin_type;
        }
    }
    return _struct;
}

export class user_query_req
{
    public token : string = "";

}

export function user_query_req_to_protcol(_struct:user_query_req){
    return _struct;
}

export function protcol_to_user_query_req(_protocol:any){
    let _struct = new user_query_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
    }
    return _struct;
}

export class user_save_req
{
    public token : string = "";
    public free_essay : common.order_essay_block[] = [];
    public pay_essay : common.order_essay_block[] = [];
    public save_state : common.save_image_state = common.save_image_state.save_public;

}

export function user_save_req_to_protcol(_struct:user_save_req){
    return _struct;
}

export function protcol_to_user_save_req(_protocol:any){
    let _struct = new user_save_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "free_essay"){
            _struct.free_essay = [];
            for(let v_ of val as any){
                _struct.free_essay.push(common.protcol_to_order_essay_block(v_));
            }
        }
        else if (key === "pay_essay"){
            _struct.pay_essay = [];
            for(let v_ of val as any){
                _struct.pay_essay.push(common.protcol_to_order_essay_block(v_));
            }
        }
        else if (key === "save_state"){
            _struct.save_state = val as common.save_image_state;
        }
    }
    return _struct;
}

export class user_query_prompt_req
{
    public prompt : string[] = [];
    public current_img_page : number = 0;

}

export function user_query_prompt_req_to_protcol(_struct:user_query_prompt_req){
    return _struct;
}

export function protcol_to_user_query_prompt_req(_protocol:any){
    let _struct = new user_query_prompt_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
        else if (key === "current_img_page"){
            _struct.current_img_page = val as number;
        }
    }
    return _struct;
}

export class user_query_personal_req
{
    public token : string = "";
    public personal : string = "";
    public last_img_guid : number = 0;

}

export function user_query_personal_req_to_protcol(_struct:user_query_personal_req){
    return _struct;
}

export function protcol_to_user_query_personal_req(_protocol:any){
    let _struct = new user_query_personal_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "personal"){
            _struct.personal = val as string;
        }
        else if (key === "last_img_guid"){
            _struct.last_img_guid = val as number;
        }
    }
    return _struct;
}

export class user_query_home_req
{
    public token : string = "";
    public last_img_guid : number = 0;

}

export function user_query_home_req_to_protcol(_struct:user_query_home_req){
    return _struct;
}

export function protcol_to_user_query_home_req(_protocol:any){
    let _struct = new user_query_home_req();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "token"){
            _struct.token = val as string;
        }
        else if (key === "last_img_guid"){
            _struct.last_img_guid = val as number;
        }
    }
    return _struct;
}

/*this caller code is codegen by abelkhan codegen for typescript*/
