import * as common from "./common";
/*this enum code is codegen by abelkhan codegen for ts*/

/*this struct code is codegen by abelkhan codegen for typescript*/
export class user_data_rsp
{
    public code : common.error = common.error.system_error;
    public data : common.user_data | null = null;
    public token : string = "";

}

export function user_data_rsp_to_protcol(_struct:user_data_rsp){
    return _struct;
}

export function protcol_to_user_data_rsp(_protocol:any){
    let _struct = new user_data_rsp();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "code"){
            _struct.code = val as common.error;
        }
        else if (key === "data"){
            _struct.data = common.protcol_to_user_data(val);
        }
        else if (key === "token"){
            _struct.token = val as string;
        }
    }
    return _struct;
}

export class user_payment_rsp
{
    public code : common.error = common.error.system_error;
    public payment_info_list : common.user_payment_info[] = [];

}

export function user_payment_rsp_to_protcol(_struct:user_payment_rsp){
    return _struct;
}

export function protcol_to_user_payment_rsp(_protocol:any){
    let _struct = new user_payment_rsp();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "code"){
            _struct.code = val as common.error;
        }
        else if (key === "payment_info_list"){
            _struct.payment_info_list = [];
            for(let v_ of val as any){
                _struct.payment_info_list.push(common.protcol_to_user_payment_info(v_));
            }
        }
    }
    return _struct;
}

export class user_query_img_rsp
{
    public code : common.error = common.error.system_error;
    public current_img_page : number = 0;
    public current_img_guid : number = 0;
    public img_list : common.user_img[] = [];

}

export function user_query_img_rsp_to_protcol(_struct:user_query_img_rsp){
    return _struct;
}

export function protcol_to_user_query_img_rsp(_protocol:any){
    let _struct = new user_query_img_rsp();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "code"){
            _struct.code = val as common.error;
        }
        else if (key === "current_img_page"){
            _struct.current_img_page = val as number;
        }
        else if (key === "current_img_guid"){
            _struct.current_img_guid = val as number;
        }
        else if (key === "img_list"){
            _struct.img_list = [];
            for(let v_ of val as any){
                _struct.img_list.push(common.protcol_to_user_img(v_));
            }
        }
    }
    return _struct;
}

export class user_save_rsp
{
    public err_code : common.error = common.error.system_error;
    public payment_info_list : common.user_payment_info[] = [];
    public img_essay : common.user_img | null = null;

}

export function user_save_rsp_to_protcol(_struct:user_save_rsp){
    return _struct;
}

export function protcol_to_user_save_rsp(_protocol:any){
    let _struct = new user_save_rsp();
    for (const [key, val] of Object.entries(_protocol)) {
        if (key === "err_code"){
            _struct.err_code = val as common.error;
        }
        else if (key === "payment_info_list"){
            _struct.payment_info_list = [];
            for(let v_ of val as any){
                _struct.payment_info_list.push(common.protcol_to_user_payment_info(v_));
            }
        }
        else if (key === "img_essay"){
            _struct.img_essay = common.protcol_to_user_img(val);
        }
    }
    return _struct;
}

/*this module code is codegen by abelkhan codegen for typescript*/
