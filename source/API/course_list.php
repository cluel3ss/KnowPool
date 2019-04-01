<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:41 PM
 */

if ($_SERVER['REQUEST_METHOD'] === 'GET')
{
    require_once('libs/initialize.php');
    /*
     * Open a DB connection and start checking the fields whether they are empty or not.
     * If they're empty, return appropriate error.
     * Also, if the field is NOT empty and we're using that data, we need to escape the special chars to avoid SQL Injection.
     */
    $db = db_connect();

    $query = "SELECT * FROM courses";
    // echo $query;
    $result = mysqli_query($db, $query);
    $my_json = array();

    if ($result->num_rows > 0)
    {
        $count  = 0;
        /* fetch associative array */
        while ($row = mysqli_fetch_assoc($result)) {

            $my_json[$count] =  array(
                'c_id' => $row["c_id"],
                'c_name' => $row["c_name"],
                'c_desc' => $row["c_desc"],
                'c_use' => $row["c_use"],
                'c_photo' => $row["c_photo"]
            );
            $count += 1;
        }

        echo json_encode(array(
            'error_code' => '0',
            'message' => 'Entries Found',
            'records' => $my_json
        ));

        db_disconnect($db, $result);
        exit(0);
    }
    else
    {
        echo json_encode(array(
            'error_code' => '1',
            'message' => 'No Entry Found'
        ));
        db_disconnect($db, $result);
        exit(1);
    }
}
else
{
    echo "<html><body><h1>Method Not Allowed</h1></body></html>";
}

?>