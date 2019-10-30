#sudo pip3 install grpcio
#sudo pip3 install grpcio-tools
#python3 -m grpc_tools.protoc -I. --python_out=. --grpc_python_out=. shopping.proto

import grpc
import google.protobuf
import shopping_pb2
import shopping_pb2_grpc

channel = grpc.insecure_channel('localhost:5000')
client = shopping_pb2_grpc.ShopKeeperStub(channel)
item = input('Item [all]: ')
if len(item) :
	info = client.GetItemInfo(shopping_pb2.ItemInfoRequest(name=item))
	print('Stock =', info.currentStock)
else:
	items = client.GetItemNames(google.protobuf.empty_pb2.Empty())
	for item in items:
		print(item.name)


	


