export enum SortColumn {
    description = 'description',
    createdDate = 'createdDate'
}

export type TodoHeaderProps = {
    sortTodo: (column: SortColumn) => void
};
