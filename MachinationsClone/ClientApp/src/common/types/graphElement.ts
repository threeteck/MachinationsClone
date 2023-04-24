export type GraphElement = {
    id: string;
    graphId: string;
    elementType: string;
    properties: Record<string, string>;
    state: Record<string, string>;
}
