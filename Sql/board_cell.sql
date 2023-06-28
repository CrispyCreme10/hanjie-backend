CREATE TABLE board_cell (
    board_id NVARCHAR(255) NOT NULL,
    row_no TINYINT NOT NULL,
    col_no TINYINT NOT NULL,
    value TINYINT NOT NULL,
    PRIMARY KEY(board_id, row_no, col_no)
);