﻿using Microsoft.AspNetCore.Mvc;
using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BendingController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public BendingController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            var shapeB1_2_B2_5_H1_5_H2_5 = new Shape()
            {
                B1 = 2,
                B2 = 5,
                H1 = 5,
                H2 = 5
            };

            var shapeB1_5_B2_2_H1_5_H2_3 = new Shape()
            {
                B1 = 5,
                B2 = 2,
                H1 = 5,
                H2 = 3
            };

            var shapeB1_4_B2_5_H1_5_H2_5 = new Shape()
            {
                B1 = 4,
                B2 = 5,
                H1 = 5,
                H2 = 5
            };

            var shapeB1_3_B2_5_H1_2_H2_2 = new Shape()
            {
                B1 = 2,
                B2 = 3,
                H1 = 3,
                H2 = 3
            };

            var shapeB1_3_B2_5_H1_3_H2_3 = new Shape()
            {
                B1 = 3,
                B2 = 5,
                H1 = 3,
                H2 = 3
            };

            BendingCommand bendingCommand = new BendingCommand()
            {
                BeamType = 4,
                Beam = new Beam() {
                        L1 = 2,
                        L2 = 3,
                        L3 = 1,
                        Q1=8,
                        Q2 = 5,
                        P = 9},
                ShapeType =2,
                Shape = shapeB1_2_B2_5_H1_5_H2_5
            };
            _commandDispatcher.DispatchAsync(bendingCommand);
            return null;
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] BendingCommand command)
        {
            _commandDispatcher.DispatchAsync(command);
            return null;
        }
    }
}
