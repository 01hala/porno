nohup dotnet ./bin/center_svr.dll ./config/config_node.txt center &
sleep 10

nohup dotnet ./bin/dbproxy_svr.dll ./config/config_node.txt dbproxy &
sleep 2

nohup dotnet ./bin/http_gate.dll ./config/config_node.txt http_gate &
sleep 2

nohup dotnet ./bin/data.dll ./config/config_node.txt data &
nohup dotnet ./bin/generate.dll ./config/config_node.txt generate &
nohup dotnet ./bin/hotspot.dll ./config/config_node.txt hotspot &
nohup dotnet ./bin/personal.dll ./config/config_node.txt personal &