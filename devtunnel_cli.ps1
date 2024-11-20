devtunnel user login

# host temporary tunnel on port 8080, https, anonymous
devtunnel host "boscc_37-demo_tunnel" -p 8080 --protocol https --allow-anonymous

# create a new tunnel, specifying a tunnel id, and a description
devtunnel create "boscc-37-demo" -a -d "BOSCC 37 Demo"