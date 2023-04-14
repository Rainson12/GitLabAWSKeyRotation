export type Guid = string & { isGuid: true};
export function guid(guid: string) : Guid {
    return  guid as Guid; // maybe add validation that the parameter is an actual guid ?
}