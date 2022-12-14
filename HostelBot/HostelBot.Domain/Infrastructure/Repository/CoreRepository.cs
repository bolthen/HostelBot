﻿// using HostelBot.Domain.Domain;
//
// namespace HostelBot.Domain.Infrastructure.Repository;
//
// public class CoreRepository
// {
//     private MainDbContext context;
//     
//     public EntityRepository<Resident> Residents { get; set; }
//     
//     public EntityRepository<Hostel> Hostels { get; set; }
//     
//     public EntityRepository<Room> Rooms { get; set; }
//     
//     public EntityRepository<Utility> Utilities { get; set; }
//
//     public CoreRepository(MainDbContext mainDbContext, EntityRepository<Resident> residents,
//         EntityRepository<Hostel> hostels, EntityRepository<Room> rooms,
//         EntityRepository<Utility> utilities)
//     {
//         context = mainDbContext;
//         Residents = residents;
//         Hostels = hostels;
//         Rooms = rooms;
//         Utilities = utilities;
//     }
// }