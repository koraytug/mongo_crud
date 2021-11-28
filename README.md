# MongoCRUD REST API

This is a example of REST API to which one use mongo db and redis caching.

## Install

    API uses redis if you do not have redis you can install with the command below
    brew install redis
    
    you can start redis 
    brew services start redis

    API uses mongoDb if you do not have mongodb you can install with the command below
    brew install mongodb-community@5.0
    brew services start mongodb-community@5.0  => to start mongo db


    or you can setup Mongodb docker
    docker run -d —-rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
    
    Docker ps   => to verify if it is running

# REST API

The REST API is described below. 

## Get list of products

### Request

`GET /produts/`

    curl --location --request GET 'http://localhost:5000/products/'

## Create a new product

### Request

`POST /products/`

    http://localhost:5000/products/

### Body

    Json
    
    {
        "name": "Döner",
        "description": "1 Porsiyon yaprak döner",
        "categoryId": "10aeda2dfe374764e33eb14b208b262f",
        "price": "25.90",
        "currency": "TL"
    }

## Get a specific product

### Request

`GET /products/id`

    curl --location --request GET 'http://localhost:5000/products/9ec2c080-923e-4bb6-ad75-8b83a21aa052'

## Update a product

### Request

`PUT /product/:id`

    http://localhost:5000/products/72030445-c50a-47ce-a95b-4c12daf578de

### Body

    Json

    {
        "name":"Döner",
        "description": "1 Porsiyon yaprak döner",
        "categoryId":"10aeda2dfe374764e33eb14b208b262f",
        "price": "25.90",
        "currency": "TL"
    }

## Delete a product

### Request

`DELETE /product/id`

    curl --location --request DELETE 'http://localhost:5000/products/9ec2c080-923e-4bb6-ad75-8b83a21aa052'
