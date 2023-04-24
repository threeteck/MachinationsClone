import {GraphElement} from "./graphElement";

enum LineType
{
    Solid = 0,
    Dashed = 1,
}

export type ConnectionType = {
    name: string;
    description: string;
    lineType: LineType;
}

export type GraphConnection = GraphElement & {
    elementType: 'connection';
    connectionType: string;
    startId: string;
    endId: string;
}
