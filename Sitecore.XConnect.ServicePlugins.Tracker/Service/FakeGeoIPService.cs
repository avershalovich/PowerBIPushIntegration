using System;
using System.Collections.Generic;
using Sitecore.XConnect.ServicePlugins.InteractionsTracker.Models;

namespace Sitecore.XConnect.ServicePlugins.InteractionsTracker
{
    public class FakeGeoIpService
    {
        static List<GeoIpData> FakeGeoIps = new List<GeoIpData>()
        {
            new GeoIpData() {
                City = "Houston",
                Country = "United States of America",
                Latitude = 29.7272,
                Longitude = -95.3408,
                IpAddress = "129.7.135.130"
            },
            new GeoIpData() {
                City = "Amsterdam",
                Country = "Netherlands",
                Latitude = 52.374030,
                Longitude = 4.889690,
                IpAddress = "77.83.46.250"
            },
            new GeoIpData() {
                City = "Milan",
                Country = "Italy",
                Latitude = 45.4655,
                Longitude = 9.18652,
                IpAddress = "217.31.113.162"
            },
            new GeoIpData() {
                City = "New York",
                Country = "United States of America",
                Latitude = 40.7056,
                Longitude = -73.978,
                IpAddress = "161.185.160.93"
            },
            new GeoIpData() {
                City = "Seattle",
                Country = "United States of America",
                Latitude = 47.6062,
                Longitude = -122.3321,
                IpAddress = "75.172.72.64"
            },
            new GeoIpData() {
                City = "Den Haag",
                Country = "Netherlands",
                Latitude = 52.07667,
                Longitude = 4.29861,
                IpAddress = "93.174.95.124"
            },
            new GeoIpData() {
                City = "Dubai",
                Country = "United Arab Emirates",
                Latitude = 25.0896,
                Longitude = 55.1529,
                IpAddress = "91.74.180.36"
            },
            new GeoIpData() {
                City = "Munich",
                Country = "Germany",
                Latitude = 48.174,
                Longitude = 11.535,
                IpAddress = "194.246.158.128"
            },
            new GeoIpData() {
                City = "California",
                Country = "United States of America",
                Latitude = 37.3861,
                Longitude = -122.084,
                IpAddress = "123.45.67.89"
            },
            new GeoIpData() {
                City = "Dublin",
                Country = "Ireland",
                Latitude = 53.3498,
                Longitude = -6.26031,
                IpAddress = "83.210.161.59"
            }
        };

        public static GeoIpData GetGeoIp(string ip)
        {
            var ran = new Random();
            return FakeGeoIps[ran.Next(0, FakeGeoIps.Count - 1)];
        }
    }
}
