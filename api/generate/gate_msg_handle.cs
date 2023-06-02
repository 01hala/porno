using abelkhan;
using MongoDB.Bson;
using service;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace generate
{
    class gate_msg_handle
    {
        private gate_gen_module _module = new();
        public gate_msg_handle()
        {
            _module.on_save_img += _module_on_save_img;
        }

        private user_order_info get_order(string order_uuid, List<user_order_info> order_list)
        {
            foreach (var order in order_list)
            {
                if (order.order_uuid == order_uuid)
                {
                    order_list.Remove(order);
                    return order;
                }
            }
            return null;
        }

        private async void _module_on_save_img(string _account, string order_uuid, save_image_state _save_state)
        {
            log.log.trace("on_save_img begin!");

            var rsp = (gate_gen_save_img_rsp)_module.rsp;

            var order_list = await generate._redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(_account));
            if (order_list == null)
            {
                rsp.err(error.img_is_expired);
                return;
            }
            var _order = get_order(order_uuid, order_list);
            if (_order == null)
            {
                rsp.err(error.img_is_expired);
                return;
            }

            rsp.rsp(_order.url);
        }
    }
}
