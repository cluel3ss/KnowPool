<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 13-03-2018
 * Time: 12:59 PM
 */

function db_connect()
{
    $connection = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME);

    if ($connection->connect_error) {
        echo json_encode(array(
            'error_code' => "1",
            'message' => "$connection->connect_error"
        ));
        die("Connection failed: " . $connection->connect_error);
    }
    return $connection;
}

function db_disconnect($connection, $result_set)
{
    if (isset($connection))
    {
//        mysqli_free_result($connection);
        mysqli_close($connection);
    }
}


?>