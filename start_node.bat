start ./bin/debug/center_svr.exe ./config/config_node.txt center &
sleep 10

start ./bin/debug/dbproxy_svr.exe ./config/config_node.txt dbproxy &
sleep 2

start ./bin/debug/http_gate.exe ./config/config_node.txt http_gate &
sleep 2

start ./bin/debug/data.exe ./config/config_node.txt data &
start ./bin/debug/generate.exe ./config/config_node.txt generate &
start ./bin/debug/hotspot.exe ./config/config_node.txt hotspot &
start ./bin/debug/personal.exe ./config/config_node.txt personal &