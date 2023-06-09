﻿// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var conn = factory.CreateConnection();
using var channel = conn.CreateModel();

channel.QueueDeclare("bookings", false, false, false, null);

var consumer = new EventingBasicConsumer(channel);


consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"New ticket processing is initial - {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();
Console.WriteLine("Hello, World!");

