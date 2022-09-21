export type TodoItemProps = {
    id: number;
    description: string;
    isComplete: boolean;
    createdDate: Date;
    modifiedDate: Date;
    onEditClick: (id: number, description: string) => void;
    onDeleteClick: (id: number) => void;
    onStatusChange: (id: number, isComplete: boolean) => void;
}