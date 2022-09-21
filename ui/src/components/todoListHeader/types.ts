export enum sortColumn {
    description = 'description',
    createdDate = 'createdDate'
}

export type todoHeaderProps = {
    sortTodo: (column: sortColumn) => void
};
