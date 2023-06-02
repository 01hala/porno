/*this enum code is codegen by abelkhan codegen for ts*/

export enum error{
    system_error = -3,
    timeout = -2,
    db_error = -1,
    success = 0,
    no_exist_user = 1,
    wrong_password = 2,
    no_enough_coin = 3,
    money_number_mismatch = 4,
    img_is_expired = 5,
    account_repeat = 6,
    mail_repeat = 7
}

export enum coin_type{
    coin = 1,
    money = 2
}

export enum gen_image_state{
    in_queue = 1,
    in_generating = 2,
    done_generate = 3
}

export enum save_image_state{
    save_public = 1,
    save_private = 2,
    give_up = 3
}

/*this struct code is codegen by abelkhan codegen for typescript*/
export class order
{
    public order_uuid : string = "";
    public account : string = "";
    public prompt : string[] = [];

}

export function order_to_protcol(_struct:order){
    return _struct;
}

export function protcol_to_order(_protocol:any){
    let _struct = new order();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "order_uuid"){
            _struct.order_uuid = val as string;
        }
        else if (key === "account"){
            _struct.account = val as string;
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

export class user_order_info
{
    public order_uuid : string = "";
    public user_name : string = "";
    public state : gen_image_state = gen_image_state.in_queue;
    public expire_time : number = 0;
    public prompt : string[] = [];
    public url : string = "";
    public img_guid : number = 0;
    public file : string = "";
    public gen_svr : string = "";

}

export function user_order_info_to_protcol(_struct:user_order_info){
    return _struct;
}

export function protcol_to_user_order_info(_protocol:any){
    let _struct = new user_order_info();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "order_uuid"){
            _struct.order_uuid = val as string;
        }
        else if (key === "user_name"){
            _struct.user_name = val as string;
        }
        else if (key === "state"){
            _struct.state = val as gen_image_state;
        }
        else if (key === "expire_time"){
            _struct.expire_time = val as number;
        }
        else if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
        else if (key === "url"){
            _struct.url = val as string;
        }
        else if (key === "img_guid"){
            _struct.img_guid = val as number;
        }
        else if (key === "file"){
            _struct.file = val as string;
        }
        else if (key === "gen_svr"){
            _struct.gen_svr = val as string;
        }
    }
    return _struct;
}

export class user_payment_info
{
    public order_uuid : string = "";
    public state : gen_image_state = gen_image_state.in_queue;
    public completeness : number = 0;
    public url : string = "";

}

export function user_payment_info_to_protcol(_struct:user_payment_info){
    return _struct;
}

export function protcol_to_user_payment_info(_protocol:any){
    let _struct = new user_payment_info();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "order_uuid"){
            _struct.order_uuid = val as string;
        }
        else if (key === "state"){
            _struct.state = val as gen_image_state;
        }
        else if (key === "completeness"){
            _struct.completeness = val as number;
        }
        else if (key === "url"){
            _struct.url = val as string;
        }
    }
    return _struct;
}

export class user_data
{
    public account : string = "";
    public name : string = "";
    public coin : number = 0;
    public level : number = 0;

}

export function user_data_to_protcol(_struct:user_data){
    return _struct;
}

export function protcol_to_user_data(_protocol:any){
    let _struct = new user_data();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "account"){
            _struct.account = val as string;
        }
        else if (key === "name"){
            _struct.name = val as string;
        }
        else if (key === "coin"){
            _struct.coin = val as number;
        }
        else if (key === "level"){
            _struct.level = val as number;
        }
    }
    return _struct;
}

export class order_essay_block
{
    public order_uuid : string = "";
    public essay : string = "";

}

export function order_essay_block_to_protcol(_struct:order_essay_block){
    return _struct;
}

export function protcol_to_order_essay_block(_protocol:any){
    let _struct = new order_essay_block();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "order_uuid"){
            _struct.order_uuid = val as string;
        }
        else if (key === "essay"){
            _struct.essay = val as string;
        }
    }
    return _struct;
}

export class essay_block
{
    public img_guid : number = 0;
    public img_url : string = "";
    public essay : string = "";

}

export function essay_block_to_protcol(_struct:essay_block){
    return _struct;
}

export function protcol_to_essay_block(_protocol:any){
    let _struct = new essay_block();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "img_guid"){
            _struct.img_guid = val as number;
        }
        else if (key === "img_url"){
            _struct.img_url = val as string;
        }
        else if (key === "essay"){
            _struct.essay = val as string;
        }
    }
    return _struct;
}

export class user_img_db
{
    public account : string = "";
    public create_time : number = 0;
    public essay_guid : number = 0;
    public free_essay : essay_block[] = [];
    public pay_essay : essay_block[] = [];
    public save_state : save_image_state = save_image_state.save_public;
    public prompt : string[] = [];
    public hotspot : number = 0;
    public discuss_count : number = 0;

}

export function user_img_db_to_protcol(_struct:user_img_db){
    return _struct;
}

export function protcol_to_user_img_db(_protocol:any){
    let _struct = new user_img_db();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "account"){
            _struct.account = val as string;
        }
        else if (key === "create_time"){
            _struct.create_time = val as number;
        }
        else if (key === "essay_guid"){
            _struct.essay_guid = val as number;
        }
        else if (key === "free_essay"){
            _struct.free_essay = [];
            for(let v_ of val as any){
                _struct.free_essay.push(protcol_to_essay_block(v_));
            }
        }
        else if (key === "pay_essay"){
            _struct.pay_essay = [];
            for(let v_ of val as any){
                _struct.pay_essay.push(protcol_to_essay_block(v_));
            }
        }
        else if (key === "save_state"){
            _struct.save_state = val as save_image_state;
        }
        else if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
        else if (key === "hotspot"){
            _struct.hotspot = val as number;
        }
        else if (key === "discuss_count"){
            _struct.discuss_count = val as number;
        }
    }
    return _struct;
}

export class hotspot_prompt_db
{
    public prompt : string = "";
    public count : number = 0;

}

export function hotspot_prompt_db_to_protcol(_struct:hotspot_prompt_db){
    return _struct;
}

export function protcol_to_hotspot_prompt_db(_protocol:any){
    let _struct = new hotspot_prompt_db();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "prompt"){
            _struct.prompt = val as string;
        }
        else if (key === "count"){
            _struct.count = val as number;
        }
    }
    return _struct;
}

export class user_img
{
    public creator : string = "";
    public creator_name : string = "";
    public create_time : number = 0;
    public essay_guid : number = 0;
    public essay : essay_block[] = [];
    public prompt : string[] = [];
    public hotspot : number = 0;
    public discuss_count : number = 0;

}

export function user_img_to_protcol(_struct:user_img){
    return _struct;
}

export function protcol_to_user_img(_protocol:any){
    let _struct = new user_img();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "creator"){
            _struct.creator = val as string;
        }
        else if (key === "creator_name"){
            _struct.creator_name = val as string;
        }
        else if (key === "create_time"){
            _struct.create_time = val as number;
        }
        else if (key === "essay_guid"){
            _struct.essay_guid = val as number;
        }
        else if (key === "essay"){
            _struct.essay = [];
            for(let v_ of val as any){
                _struct.essay.push(protcol_to_essay_block(v_));
            }
        }
        else if (key === "prompt"){
            _struct.prompt = [];
            for(let v_ of val as any){
                _struct.prompt.push(v_);
            }
        }
        else if (key === "hotspot"){
            _struct.hotspot = val as number;
        }
        else if (key === "discuss_count"){
            _struct.discuss_count = val as number;
        }
    }
    return _struct;
}

/*this module code is codegen by abelkhan codegen for typescript*/
