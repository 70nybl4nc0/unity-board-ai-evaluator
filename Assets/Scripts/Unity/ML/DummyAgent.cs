using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using InteligenceEngine;
using System;
using System.Linq;
using Unity.MLAgents.Policies;

public class DummyAgent : BaseAgent
{

    BehaviorParameters behaviour;

    protected void Awake()
    {
        behaviour = GetComponent<BehaviorParameters>();
        behaviour.BrainParameters.VectorObservationSize = Game.width * Game.height * 3;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        for (int x = 0; x < controller.board.width; x++)
        {
            for (int y = 0; y < controller.board.height; y++)
            {
                sensor.AddObservation(new Vector3Int(x, y, controller.board[x, y]));
            }
        }
    }

    public override void AddRewards()
    {
        AddReward(-1);

        var gameState = controller.state;

        if (controller.lastPoints > 0)
        {
            AddReward(  10 * controller.lastPoints);
        }

        if (gameState == Game.State.Win)
        {
            AddReward(40);
        }

        if (gameState == Game.State.Lose)
        {
            AddReward(-20);
        }
    }


}
