import {GraphElement} from "./graphElement";

export enum ActivationMode {
    Auto = 0,
    Passive = 1,
    OnStart = 2
}

export enum PullMode
{
    PullAll = 0,
    PullAny = 1,
    PushAll = 2,
    PushAny = 3,
}

export type NodeType = {
    name: string;
    description: string;
    symbol: string;
    exportable: boolean;
}

export type GraphNode = GraphElement & {
    elementType: 'node';
    name: string;
    nodeType: string;
    activationMode: ActivationMode;
    pullMode: PullMode;
    geometry?: NodeGeometry | null;
}

export type NodeGeometry = {
    id: string;
    nodeId: string;
    x: number;
    y: number;
}
