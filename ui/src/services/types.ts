export interface ResponseModel<T> {
    errorCode: number;
    errorMessage: string;
    result: T;
}

export interface TodoRequestModel {
    description: string;
}

export interface StatusUpdateTodoModel {
    isComplete: boolean;
}