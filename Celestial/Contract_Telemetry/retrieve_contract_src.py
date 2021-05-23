from etherscan.contracts import Contract
import csv

key = "etherscan_api_key"

filepath = "path_to_csv_file_of_addresses_from_google_bigquery"

with open(filepath) as csv_file:
    csv_reader = csv.reader(csv_file, delimiter=',')
    line_count = 0
    err = 0
    for row in csv_reader:
        address = row[0]
        print(address)
        
        api = Contract(address=address, api_key=key)
        try:     
            sourcecode = api.get_sourcecode()
            
            completeName = address+'.sol'
            filehandle = open(completeName,"w") 
            buf = sourcecode[0]['SourceCode']
            filehandle.writelines(buf) 
            filehandle.close() 
            
        except:
            err = err+1
        line_count += 1

    print(f'Processed {line_count} lines.')
    print('Errors:', err)