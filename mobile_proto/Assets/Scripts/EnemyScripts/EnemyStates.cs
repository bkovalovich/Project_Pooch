using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//[SerializeReference]
enum State {
    None,
    EnemyBasicIdleState,
    EnemyBasicChaseState,
    EnemyBasicDestroyState
}
public static class EnemyStates {

    public static Type basicIdleState = typeof(EnemyBasicIdleState);
    public static Type basicChaseState = typeof(EnemyBasicChaseState);
    public static Type basicDestroyState = typeof(EnemyBasicDestroyState);

    private static Type[] allTypes = { basicIdleState, basicChaseState, basicDestroyState };

    public static Type GetState(string s) {
        if (s == "None") { return null; }

        foreach(Type type in allTypes) {
            if(s == type.ToString()) {
                return type;
            }
        }
        return null; 
    }
}
